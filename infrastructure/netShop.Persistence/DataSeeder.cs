using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netShop.Persistence.Content;

namespace netShop.Persistence
{
    public static class DataSeeder
    {
        public static IHost SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();
                if (!context.Products.Any())
                {
                    #region Products
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P001",
                        productName = "Apple Macbook Pro 13`",
                        description = "Apple Macbook Pro 13 inch 2015",
                        price = 13500,
                        quantity = 235
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P002",
                        productName = "Apple Macbook Pro 15`",
                        description = "Apple Macbook Pro 15 inch 20220",
                        price = 15600,
                        quantity = 265
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P003",
                        productName = "Apple Macbook Pro 17`",
                        description = "Apple Macbook Pro 17 inch 2018",
                        price = 13450,
                        quantity = 250
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P004",
                        productName = "Apple Macbook Pro 19`",
                        description = "Apple Macbook Pro 19 inch 2017",
                        price = 12999,
                        quantity = 240
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P005",
                        productName = "Apple Macbook Air 13`",
                        description = "Apple Macbook Air 13 inch 2021",
                        price = 15400,
                        quantity = 230
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P006",
                        productName = "Apple Macbook Air 15`",
                        description = "Apple Macbook Air 15 inch 2015",
                        price = 17250,
                        quantity = 145
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P007",
                        productName = "Apple Macbook Air 17`",
                        description = "Apple Macbook Air 17` inch 2021",
                        price = 1600,
                        quantity = 200
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P008",
                        productName = "Apple Macbook Air 19`",
                        description = "Apple Macbook Air 19 inch 2020",
                        price = 1345,
                        quantity = 205
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P009",
                        productName = "Asus Zenbook A8",
                        description = "Asus Zenbook A8 13 inch",
                        price = 12345,
                        quantity = 155
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0010",
                        productName = "Asus Zenbook A9",
                        description = "Asus Zenbook A9 15 inch",
                        price = 11234,
                        quantity = 289
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0011",
                        productName = "Asus Zenbook A11",
                        description = "Asus Zenbook A11 14.1 inch",
                        price = 9345,
                        quantity = 677
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0012",
                        productName = "Lenovo E15",
                        description = "Lenovo E15 Amd Ryzen5 4500U 40GB 512GB SSD+1TB SSD Freedos 15.6\" FHD Taşınabilir Bilgisayar 20T80021TX24",
                        price = 12000,
                        quantity = 123
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0013",
                        productName = "HP 15-DW3017NT",
                        description = "HP 15-DW3017NT Intel Core I3 1115G4 4GB 256 GB SSD Freedos 15.6\" FHD Taşınabilir Bilgisayar 2N2R4EA",
                        price = 11234,
                        quantity = 344
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0014",
                        productName = "HP 15S-FQ2050NT",
                        description = "HP 15S-FQ2050NT Intel Core i3 1125G4 4GB 128GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar 481H5EA",
                        price = 7894,
                        quantity = 235
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0015",
                        productName = "Dell 3510-N011L351015EMEA_U I5-10210U",
                        description = "Dell 3510-N011L351015EMEA_U I5-10210U 8 GB 256 GB SSD 15.6\" Free Dos Dizüstü Bilgisayar",
                        price = 13658,
                        quantity = 189
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0016",
                        productName = "Dell Vostro 3400",
                        description = "Dell Vostro 3400 Intel Core i5 1135G7 8GB 256GB SSD MX330 Ubuntu 14\" FHD Taşınabilir Bilgisayar FB1135F82N",
                        price = 6938,
                        quantity = 300
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0017",
                        productName = "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U",
                        description = "Hp Zbook FIREFLY15 1J3P7EA06 I7-10510U 64GB 512SSD P520 15.6\" Fullhd W10P Taşınabilir Iş Istasyonu",
                        price = 19848,
                        quantity = 124
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0018",
                        productName = "Lenovo V15 IIL",
                        description = "Lenovo V15 IIL Intel Core i3 1005G1 8GB 256GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar 82C500QTTX + Microsoft 365 1 Yıllık Dijital Bireysel",
                        price = 5899,
                        quantity = 289
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0019",
                        productName = "Asus X515JP EJ250A13",
                        description = "Asus X515JP EJ250A13 Intel Core I7 1065G7 8gb 512GB SSD MX330 Windows 10 Home 15.6\" Fhd Taşınabilir Bilgisayar",
                        price = 11317,
                        quantity = 432
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0020",
                        productName = "Lenovo Ideapad 3",
                        description = "Lenovo Ideapad 3 Intel Core i5 10210U 8GB 512GB SSD MX130 Freedos 15.6\" FHD Taşınabilir Bilgisayar 81WB00S8TX",
                        price = 21300,
                        quantity = 400
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0021",
                        productName = "I-Life ZED Air CX7",
                        description = "I-Life ZED Air CX7 Intel Core i7 7Y75 8GB 256GB SSD Windows 10 Home 15.6\" FHD Taşınabilir Bilgisayar NTBTILWBI7158256",
                        price = 11100,
                        quantity = 272
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0022",
                        productName = "Acer Extensa 15",
                        description = "Acer Extensa 15 Intel Core I5 1035G1 8GB 512GB SSD MX330 Freedos 15.6\" FHD Taşınabilir Bilgisayar NX.EGCEY.002",
                        price = 15670,
                        quantity = 433
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0023",
                        productName = "Lenovo IdeaPad 5",
                        description = "Lenovo IdeaPad 5 Intel Core i5 1135G7 8GB 256GB SSD Windows 10 Home 14\" FHD Taşınabilir Bilgisayar 82FE00KGTX",
                        price = 12350,
                        quantity = 242
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0024",
                        productName = "Asus ROG Strix G513IH-HN002",
                        description = "Asus ROG Strix G513IH-HN002 AMD Ryzen 7 4800H 8GB 512GB SSD GTX 1650 Freedos 15.6\" FHD Taşınabilir Bilgisayar",
                        price = 13500,
                        quantity = 450
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
                        productCode = "P0025",
                        productName = "Lenovo IdeaPad S145-15API",
                        description = "Lenovo IdeaPad S145-15API AMD Athlon 300U 8GB 256GB SSD 15.6\" Taşınabilir Bilgisayar 81UT00GDTX",
                        price = 17860,
                        quantity = 111
                    });
                    context.Products.Add(new Domain.Entities.Product()
                    {
                        Id = Guid.NewGuid(),
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