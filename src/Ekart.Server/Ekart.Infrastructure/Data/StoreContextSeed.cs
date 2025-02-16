using Ekart.Core.Entites;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Ekart.Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!await context.Products.AnyAsync())
            {
                var productData = await File.ReadAllTextAsync("../Ekart.Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);
                
                if (products == null) return;

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
