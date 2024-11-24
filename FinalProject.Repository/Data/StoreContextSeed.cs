using FinalProject.Core.Models;
using FinalProject.Core.Order_Aggregrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalProject.Repository.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            var brandData = File.ReadAllText("../FinalProject.Repository/Data/DataSeeding/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

            if (brands.Count() > 0)
            {
                
                if (_dbcontext.Brands.Count() == 0)
                {

                    foreach (var brand in brands)
                    {
                        _dbcontext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbcontext.SaveChangesAsync();

                }
            }

            var categoryData = File.ReadAllText("../FinalProject.Repository/Data/DataSeeding/categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoryData);

            if (categories.Count() > 0)
            {
                
                if (_dbcontext.Categories.Count() == 0)
                {

                    foreach (var category in categories)
                    {
                        _dbcontext.Set<ProductCategory>().Add(category);
                    }
                    await _dbcontext.SaveChangesAsync();

                }

            }

            var ProductData = File.ReadAllText("../FinalProject.Repository/Data/DataSeeding/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductData);

                if (products.Count() > 0)
                {

                if (_dbcontext.Products.Count() == 0)

                {

                    foreach (var product in products)
                        {
                        _dbcontext.Set<Product>().Add(product);
                        }
                
                    await _dbcontext.SaveChangesAsync();

                }

            }

            var DeliveryData = File.ReadAllText("../FinalProject.Repository/Data/DataSeeding/delivery.json");
            var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

            if (deliveryMethods?.Count() > 0)
            {

                if (_dbcontext.DeliveryMethods.Count() == 0)

                {

                    foreach (var method in deliveryMethods)
                    {
                        _dbcontext.Set<DeliveryMethod>().Add(method);
                    }

                    await _dbcontext.SaveChangesAsync();

                }

            }
        }
    }
}


