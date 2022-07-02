using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using application.Interfaces.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using NetShop.ProductService.Application;
using NetShop.ProductService.Infrastructure.Persistence;
using NetShop.ProductService.WebApi.Middlewares;
using Newtonsoft.Json;
using Serilog;
using webApi.Middlewares;
using webApi.Settings;

namespace NetShop.ProductService.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration configuration { get; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApplicationRegistration();
            services.AddPersistenceRegistration(configuration);

            SsoSettings ssoSettings = configuration.GetSection(nameof(SsoSettings)).Get<SsoSettings>();
            
            Log.Debug($"{nameof(SsoSettings)}: {Environment.NewLine}{JsonConvert.SerializeObject(ssoSettings, Formatting.Indented)}");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    // IdentityServer uygulamasının adresi
                    options.Authority = ssoSettings.Authority;
                    // options.RequireHttpsMetadata = true;
                    options.Audience = ssoSettings.ValidAudience;
                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = ssoSettings.ValidAudience,
                        ValidateAudience = ssoSettings.ValidateAudience,

                        ValidIssuer = ssoSettings.ValidIssuer,
                        ValidateIssuer = ssoSettings.ValidateIssuer,
                        ValidateLifetime = ssoSettings.ValidateLifetime,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ssoSettings.IssuerSigningKey)),
                        ValidateIssuerSigningKey = ssoSettings.ValidateIssuerSigningKey,
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        RoleClaimType = "role"
                    };
                    options.SaveToken = true;

                    //JWT eventlarının yakalandığı yerdir.
                    options.Events = new JwtBearerEvents
                    {
                        //Eğer Token bilgisi yanlışsa buraya düşecek.
                        OnAuthenticationFailed = _ =>
                        {
                            Log.Error($"Exception:{_.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        //Eğer token bilgisi doğruysa buraya düşecek.
                        OnTokenValidated = _ =>
                        {
                            Log.Debug($"Login Success:{_.Principal.Identity}");
                            return Task.CompletedTask;
                        },
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    // JWT token içerisinde sub claim (user info) ı olmasını zorunlu kılıyor
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", $"{ssoSettings.ValidAudience}.Read", $"{ssoSettings.ValidAudience}.Write");
                });
                options.AddPolicy("ReadApi", policy => policy.RequireClaim("scope", $"{ssoSettings.ValidAudience}.Read"));
                options.AddPolicy("WriteApi", policy => policy.RequireClaim("scope", $"{ssoSettings.ValidAudience}.Write"));
            });

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCORS", builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });

            services.AddControllers()
                    .AddNewtonsoftJson();
            services.AddHealthChecks();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "netShop.ProductService.WebApi", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogDebug("UseHttps: " + configuration["UseHttps"].ToUpper());
            if (!String.IsNullOrEmpty(configuration["UseHttps"]) && configuration["UseHttps"].ToUpper() == "YES")
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                logger.LogDebug("Https Redirection is enabled.");
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "netShop.ProductService.WebApi v1"));
            }

            app.UseCustomExceptionHandler();

            app.UseHealthChecks("/api/healthcheck", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    await context.Response.WriteAsync("OK");
                }
            });

            app.UseSecurityHeaders();
            app.UseAntiXssMiddleware();
            app.UseAntiXss2Middleware();

            app.UseRouting();

            app.UseCors("EnableCORS");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers()
                         .RequireAuthorization("ApiScope"); ;
            });
        }
    }
}
