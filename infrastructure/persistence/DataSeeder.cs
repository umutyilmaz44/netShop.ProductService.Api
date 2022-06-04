using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using NetShop.ProductService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using persistence.Settings;

namespace NetShop.ProductService.Infrastructure.Persistence
{
    public static class DataSeeder
    {
        public static async Task<IHost> SeedData(this IHost host, IConfiguration configuration)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                DbSettings dbSettings = configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();

                if (!string.IsNullOrEmpty(dbSettings.DatabaseType) &&
                    string.Equals(dbSettings.DatabaseType, "Postgresql", StringComparison.InvariantCultureIgnoreCase))
                {
                    await context.Database.MigrateAsync();
                }
                else
                {
                    await context.Database.EnsureCreatedAsync();
                }

                if (!context.Suppliers.Any())
                {
                    #region Supplier
                    context.Suppliers.Add(new Domain.Entities.Supplier()
                    {
                        Id = Guid.NewGuid(),
                        supplierName = "Woonenzo",
                        description = "Woonenzo Eindhoven",
                        email = "info@woonenzo.nl",
                        fax = "+31 088 188 18 18",
                        phone = "+31 06 34 44 99 00",
                        website = "woonenzo.nl"
                    });

                    context.Suppliers.Add(new Domain.Entities.Supplier()
                    {
                        Id = Guid.NewGuid(),
                        supplierName = "Mantis",
                        description = "Mantis Ankara",
                        email = "info@mantis.com.tr",
                        fax = "+90 312 222 33 44",
                        phone = "+90 06 06 44 23 31",
                        website = "mantis.com.tr"
                    });

                    context.Suppliers.Add(new Domain.Entities.Supplier()
                    {
                        Id = Guid.NewGuid(),
                        supplierName = "VHS",
                        description = "VHS Ositm",
                        email = "info@vhs.com.tr",
                        fax = "+90 312 444 55 66",
                        phone = "+90 12 23 34 45 56",
                        website = "vhs.com.tr"
                    });

                    Supplier supplierWoonenzo = context.ChangeTracker.Entries<Supplier>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Supplier)
                                               .Where(t => t.supplierName == "Woonenzo")
                                               .FirstOrDefault();
                    Supplier supplierMantis = context.ChangeTracker.Entries<Supplier>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Supplier)
                                               .Where(t => t.supplierName == "Mantis")
                                               .FirstOrDefault();
                    Supplier supplierVhs = context.ChangeTracker.Entries<Supplier>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Supplier)
                                               .Where(t => t.supplierName == "VHS")
                                               .FirstOrDefault();
                    #endregion

                    #region Brand
                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Apple",
                        description = "Apple Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Asus",
                        description = "Asus Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Lenovo",
                        description = "Lenovo Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Lenovo",
                        description = "Lenovo Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Hp",
                        description = "Hp Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Dell",
                        description = "Dell Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "ILife",
                        description = "ILife Inc."
                    });

                    context.Brands.Add(new Domain.Entities.Brand()
                    {
                        Id = Guid.NewGuid(),
                        brandName = "Acer",
                        description = "Acer Inc."
                    });

                    Brand brandApple = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "Apple")
                                               .FirstOrDefault();

                    Brand brandAsus = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "Asus")
                                               .FirstOrDefault();


                    Brand brandLenovo = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "Lenovo")
                                               .FirstOrDefault();
                    Brand brandHp = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "Hp")
                                               .FirstOrDefault();
                    Brand brandDell = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "Dell")
                                               .FirstOrDefault();
                    Brand brandILife = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "ILife")
                                               .FirstOrDefault(); ;
                    Brand brandAcer = context.ChangeTracker.Entries<Brand>()
                                               .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                               .Select(s => s.Entity as Brand)
                                               .Where(t => t.brandName == "Acer")
                                               .FirstOrDefault();
                    #endregion

                    #region Brand Model
                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brand = brandApple,
                        modelName = "Macbook Pro",
                        description = "Apple Macbook Pro Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandApple.Id,
                        modelName = "Macbook Air",
                        description = "Apple Macbook Air Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAsus.Id,
                        modelName = "Zeenbook A7",
                        description = "Asus Zeenbook A7 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAsus.Id,
                        modelName = "Zeenbook A8",
                        description = "Asus Zeenbook A8 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAsus.Id,
                        modelName = "Zeenbook A9",
                        description = "Asus Zeenbook A9 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAsus.Id,
                        modelName = "Zeenbook A11",
                        description = "Asus Zeenbook A11 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandLenovo.Id,
                        modelName = "Lenovo-E15",
                        description = "Lenovo E15 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandHp.Id,
                        modelName = "HP 15-DW3017NT",
                        description = "HP 15-DW3017NT Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandHp.Id,
                        modelName = "HP 15S-FQ2050NT",
                        description = "HP 15S-FQ2050NT Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandHp.Id,
                        modelName = "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U",
                        description = "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandDell.Id,
                        modelName = "Dell 3510-N011L351015EMEA_U I5-10210U",
                        description = "Dell 3510-N011L351015EMEA_U I5-10210U Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandDell.Id,
                        modelName = "Dell Vostro 3400",
                        description = "Dell Vostro 3400 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAsus.Id,
                        modelName = "Asus X515JP EJ250A13",
                        description = "Asus X515JP EJ250A13 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAsus.Id,
                        modelName = "Asus ROG Strix G513IH-HN002",
                        description = "Asus ROG Strix G513IH-HN002 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandLenovo.Id,
                        modelName = "Lenovo Ideapad 3",
                        description = "Lenovo Ideapad 3 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandLenovo.Id,
                        modelName = "Lenovo Ideapad 5",
                        description = "Lenovo Ideapad 5 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandLenovo.Id,
                        modelName = "Lenovo V15 IIL",
                        description = "Lenovo V15 IIL Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandLenovo.Id,
                        modelName = "Lenovo IdeaPad S145-15API",
                        description = "Lenovo IdeaPad S145-15API Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandILife.Id,
                        modelName = "I-Life ZED Air CX7",
                        description = "I-Life ZED Air CX7 Notebook"
                    });

                    context.BrandModels.Add(new Domain.Entities.BrandModel()
                    {
                        Id = Guid.NewGuid(),
                        brandId = brandAcer.Id,
                        modelName = "Acer Extensa 15",
                        description = "Acer Extensa 15 Notebook"
                    });

                    BrandModel brandModelMacbookPro = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Macbook Pro")
                                                              .FirstOrDefault();
                    BrandModel brandModelMacbookAir = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Macbook Air")
                                                              .FirstOrDefault();
                    BrandModel brandModelZeenbookA7 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Zeenbook A7")
                                                              .FirstOrDefault();
                    BrandModel brandModelZeenbookA8 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Zeenbook A8")
                                                              .FirstOrDefault();
                    BrandModel brandModelZeenbookA9 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Zeenbook A9")
                                                              .FirstOrDefault();
                    BrandModel brandModelZeenbookA11 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Zeenbook A11")
                                                              .FirstOrDefault();
                    BrandModel brandModelLenovoE15 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Lenovo-E15")
                                                              .FirstOrDefault();
                    BrandModel brandModelHp15_DW3017NT = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "HP 15-DW3017NT")
                                                              .FirstOrDefault();
                    BrandModel brandModelHp15S_FQ2050NT = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "HP 15S-FQ2050NT")
                                                              .FirstOrDefault();
                    BrandModel brandModelHpZbookFIREFLY15 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U")
                                                              .FirstOrDefault();
                    BrandModel brandModelDell3510 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Dell 3510-N011L351015EMEA_U I5-10210U")
                                                              .FirstOrDefault();
                    BrandModel brandModelDellVostro3400 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Dell Vostro 3400")
                                                              .FirstOrDefault();
                    BrandModel brandModelAsusX515JP = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Asus X515JP EJ250A13")
                                                              .FirstOrDefault();
                    BrandModel brandModelAsusROG_Strix_G513IH_HN002 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Asus ROG Strix G513IH-HN002")
                                                              .FirstOrDefault();
                    BrandModel brandModelLenovoIdeapad3 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Lenovo Ideapad 3")
                                                              .FirstOrDefault();
                    BrandModel brandModelLenovoIdeapad5 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Lenovo Ideapad 5")
                                                              .FirstOrDefault();
                    BrandModel brandModelLenovoV15IIL = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Lenovo V15 IIL")
                                                              .FirstOrDefault();
                    BrandModel brandModelLenovoIdeaPad_S145_15API = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Lenovo IdeaPad S145-15API")
                                                              .FirstOrDefault();
                    BrandModel brandModelILifeZEDAirCX7 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "I-Life ZED Air CX7")
                                                              .FirstOrDefault();
                    BrandModel brandModelAcerExtensa15 = context.ChangeTracker.Entries<BrandModel>()
                                                              .Where(x => x.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                                                              .Select(s => s.Entity as BrandModel)
                                                              .Where(t => t.modelName == "Acer Extensa 15")
                                                              .FirstOrDefault();
                    #endregion

                    #region Products
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookPro.Id,
                        productCode = "P001",
                        productName = "Apple Macbook Pro 13`",
                        description = "Apple Macbook Pro 13 inch 2015",
                        price = 13500,
                        quantity = 235
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookPro.Id,
                        productCode = "P002",
                        productName = "Apple Macbook Pro 15`",
                        description = "Apple Macbook Pro 15 inch 20220",
                        price = 15600,
                        quantity = 265
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookPro.Id,
                        productCode = "P003",
                        productName = "Apple Macbook Pro 17`",
                        description = "Apple Macbook Pro 17 inch 2018",
                        price = 13450,
                        quantity = 250
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookPro.Id,
                        productCode = "P004",
                        productName = "Apple Macbook Pro 19`",
                        description = "Apple Macbook Pro 19 inch 2017",
                        price = 12999,
                        quantity = 240
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        productCode = "P005",
                        brandModelId = brandModelMacbookAir.Id,
                        productName = "Apple Macbook Air 13`",
                        description = "Apple Macbook Air 13 inch 2021",
                        price = 15400,
                        quantity = 230
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookAir.Id,
                        productCode = "P006",
                        productName = "Apple Macbook Air 15`",
                        description = "Apple Macbook Air 15 inch 2015",
                        price = 17250,
                        quantity = 145
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookAir.Id,
                        productCode = "P007",
                        productName = "Apple Macbook Air 17`",
                        description = "Apple Macbook Air 17` inch 2021",
                        price = 1600,
                        quantity = 200
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelMacbookAir.Id,
                        productCode = "P008",
                        productName = "Apple Macbook Air 19`",
                        description = "Apple Macbook Air 19 inch 2020",
                        price = 1345,
                        quantity = 205
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelZeenbookA8.Id,
                        productCode = "P009",
                        productName = "Asus Zenbook A8",
                        description = "Asus Zenbook A8 13 inch",
                        price = 12345,
                        quantity = 155
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierWoonenzo.Id,
                        brandModelId = brandModelZeenbookA8.Id,
                        productCode = "P0010",
                        productName = "Asus Zenbook A9",
                        description = "Asus Zenbook A9 15 inch",
                        price = 11234,
                        quantity = 289
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelZeenbookA11.Id,
                        productCode = "P0011",
                        productName = "Asus Zenbook A11",
                        description = "Asus Zenbook A11 14.1 inch",
                        price = 9345,
                        quantity = 677
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelLenovoE15.Id,
                        productCode = "P0012",
                        productName = "Lenovo E15",
                        description = "Lenovo E15 Amd Ryzen5 4500U 40GB 512GB SSD+1TB SSD Freedos 15.6\" FHD Taşınabilir Bilgisayar 20T80021TX24",
                        price = 12000,
                        quantity = 123
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelHp15_DW3017NT.Id,
                        productCode = "P0013",
                        productName = "HP 15-DW3017NT",
                        description = "HP 15-DW3017NT Intel Core I3 1115G4 4GB 256 GB SSD Freedos 15.6\" FHD Taşınabilir Bilgisayar 2N2R4EA",
                        price = 11234,
                        quantity = 344
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelHp15S_FQ2050NT.Id,
                        productCode = "P0014",
                        productName = "HP 15S-FQ2050NT",
                        description = "HP 15S-FQ2050NT Intel Core i3 1125G4 4GB 128GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar 481H5EA",
                        price = 7894,
                        quantity = 235
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelDell3510.Id,
                        productCode = "P0015",
                        productName = "Dell 3510-N011L351015EMEA_U I5-10210U",
                        description = "Dell 3510-N011L351015EMEA_U I5-10210U 8 GB 256 GB SSD 15.6\" Free Dos Dizüstü Bilgisayar",
                        price = 13658,
                        quantity = 189
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelDellVostro3400.Id,
                        productCode = "P0016",
                        productName = "Dell Vostro 3400",
                        description = "Dell Vostro 3400 Intel Core i5 1135G7 8GB 256GB SSD MX330 Ubuntu 14\" FHD Taşınabilir Bilgisayar FB1135F82N",
                        price = 6938,
                        quantity = 300
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelHpZbookFIREFLY15.Id,
                        productCode = "P0017",
                        productName = "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U",
                        description = "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U 64GB 512SSD P520 15.6\" Fullhd W10P Taşınabilir Iş Istasyonu",
                        price = 19848,
                        quantity = 124
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,

                        brandModelId = brandModelLenovoV15IIL.Id,
                        productCode = "P0018",
                        productName = "Lenovo V15 IIL",
                        description = "Lenovo V15 IIL Intel Core i3 1005G1 8GB 256GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar 82C500QTTX + Microsoft 365 1 Yıllık Dijital Bireysel",
                        price = 5899,
                        quantity = 289
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelAsusX515JP.Id,
                        productCode = "P0019",
                        productName = "Asus X515JP EJ250A13",
                        description = "Asus X515JP EJ250A13 Intel Core I7 1065G7 8gb 512GB SSD MX330 Windows 10 Home 15.6\" Fhd Taşınabilir Bilgisayar",
                        price = 11317,
                        quantity = 432
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierMantis.Id,
                        brandModelId = brandModelLenovoIdeapad3.Id,
                        productCode = "P0020",
                        productName = "Lenovo Ideapad 3",
                        description = "Lenovo Ideapad 3 Intel Core i5 10210U 8GB 512GB SSD MX130 Freedos 15.6\" FHD Taşınabilir Bilgisayar 81WB00S8TX",
                        price = 21300,
                        quantity = 400
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierVhs.Id,
                        brandModelId = brandModelILifeZEDAirCX7.Id,
                        productCode = "P0021",
                        productName = "I-Life ZED Air CX7",
                        description = "I-Life ZED Air CX7 Intel Core i7 7Y75 8GB 256GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar NTBTILWBI7158256",
                        price = 11100,
                        quantity = 272
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierVhs.Id,
                        brandModelId = brandModelAcerExtensa15.Id,
                        productCode = "P0022",
                        productName = "Acer Extensa 15",
                        description = "Acer Extensa 15 Intel Core I5 1035G1 8GB 512GB SSD MX330 Freedos 15.6\" FHD Taşınabilir Bilgisayar NX.EGCEY.002",
                        price = 15670,
                        quantity = 433
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierVhs.Id,
                        brandModelId = brandModelLenovoIdeapad5.Id,
                        productCode = "P0023",
                        productName = "Lenovo IdeaPad 5",
                        description = "Lenovo IdeaPad 5 Intel Core i5 1135G7 8GB 256GB SSD Windows 10 Home 14\" FHD Taşınabilir Bilgisayar 82FE00KGTX",
                        price = 12350,
                        quantity = 242
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierVhs.Id,
                        brandModelId = brandModelAsusROG_Strix_G513IH_HN002.Id,
                        productCode = "P0024",
                        productName = "Asus ROG Strix G513IH-HN002",
                        description = "Asus ROG Strix G513IH-HN002 AMD Ryzen 7 4800H 8GB 512GB SSD GTX 1650 Freedos 15.6\" FHD Taşınabilir Bilgisayar",
                        price = 13500,
                        quantity = 450
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierVhs.Id,
                        brandModelId = brandModelLenovoIdeaPad_S145_15API.Id,
                        productCode = "P0025",
                        productName = "Lenovo IdeaPad S145-15API",
                        description = "Lenovo IdeaPad S145-15API AMD Athlon 300U 8GB 256GB SSD 15.6\" Taşınabilir Bilgisayar 81UT00GDTX",
                        price = 17860,
                        quantity = 111
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        supplierId = supplierVhs.Id,
                        brandModelId = brandModelZeenbookA7.Id,
                        productCode = "P0026",
                        productName = "Asus Zenbook A7",
                        description = "Asus Zenbook A7 17 inch",
                        price = 13456,
                        quantity = 455
                    });
                    #endregion

                    context.SaveChanges();
                }
            }

            return host;
        }
    }
}