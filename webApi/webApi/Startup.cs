using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
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
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using NetShop.ProductService.Application;
using NetShop.ProductService.Infrastructure.Persistence;
using NetShop.ProductService.WebApi.Middlewares;
using webApi.Middlewares;

namespace NetShop.ProductService.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationRegistration();
            services.AddPersistenceRegistration(Configuration);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    // IdentityServer uygulamasının adresi
                    options.Authority = (string)Convert.ChangeType(Configuration["JwtAuthentication:Authority"], typeof(string));
                    // options.RequireHttpsMetadata = true;
                    options.Audience = (string)Convert.ChangeType(Configuration["JwtAuthentication:ValidAudience"], typeof(string));
                    // IdentityServer emits a typ header by default, recommended extra check
                    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = (string)Convert.ChangeType(Configuration["JwtAuthentication:ValidAudience"], typeof(string)),
                        ValidateAudience = (bool)Convert.ChangeType(Configuration["JwtAuthentication:ValidateAudience"], typeof(bool)),

                        ValidIssuer = (string)Convert.ChangeType(Configuration["JwtAuthentication:ValidIssuer"], typeof(string)),
                        ValidateIssuer = (bool)Convert.ChangeType(Configuration["JwtAuthentication:ValidateIssuer"], typeof(bool)),
                        ValidateLifetime = (bool)Convert.ChangeType(Configuration["JwtAuthentication:ValidateLifetime"], typeof(bool)),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes((string)Convert.ChangeType(Configuration["JwtAuthentication:IssuerSigningKey"], typeof(string)))),
                        ValidateIssuerSigningKey = (bool)Convert.ChangeType(Configuration["JwtAuthentication:ValidateIssuerSigningKey"], typeof(bool)),
                        ClockSkew = TimeSpan.Zero,
                        RequireSignedTokens = true,
                        RequireExpirationTime = true,
                        RoleClaimType = "role"
                    };
                    options.SaveToken= true;

                    //JWT eventlarının yakalandığı yerdir.
                    options.Events = new JwtBearerEvents
                    {
                        //Eğer Token bilgisi yanlışsa buraya düşecek.
                        OnAuthenticationFailed = _ =>
                        {
                            Console.WriteLine($"Exception:{_.Exception.Message}");
                            return Task.CompletedTask;
                        },
                        //Eğer token bilgisi doğruysa buraya düşecek.
                        OnTokenValidated = _ =>
                        {
                            Console.WriteLine($"Login Success:{ _.Principal.Identity}");
                            return Task.CompletedTask;
                        },
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "ProductService.Read", "ProductService.Write");
                });
                options.AddPolicy("ReadApi", policy => policy.RequireClaim("scope", "ProductService.Read"));
                options.AddPolicy("WriteApi", policy => policy.RequireClaim("scope", "ProductService.Write"));
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseHttpsRedirection();

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
                         .RequireAuthorization("ApiScope");;
            });
        }
    }
}
