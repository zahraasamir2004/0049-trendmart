using FurniStyle.Core.Entities;
using FurniStyle.Core.Entities.OrderEntities;
using FurniStyle.Repository.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Data.DataSeeding
{
    public static class FurniStyleDataSeeding
    {
        public async static Task SeedingData(FurniStyleDbContext _furniStyleDbContext)
        {
            await SeedEntitiesAsync<Category>(_furniStyleDbContext, _furniStyleDbContext.Categories, "categories.json");
            await SeedEntitiesAsync<Room>(_furniStyleDbContext, _furniStyleDbContext.Rooms, "rooms.json");
            await SeedEntitiesAsync<Furni>(_furniStyleDbContext, _furniStyleDbContext.Furnis, "furnis.json");
            await SeedEntitiesAsync<DelivaryMethod>(_furniStyleDbContext, _furniStyleDbContext.DelivaryMethods, "delivery.json");
        }
        private static async Task SeedEntitiesAsync<T>(FurniStyleDbContext dbContext, DbSet<T> dbSet, string fileName) where T : class
        {
            if (!await dbSet.AnyAsync())
            {
                var filePath = Path.Combine("..", "FurniStyle.Repository", "Data", "DataSeeding", fileName);
                if (File.Exists(filePath))
                {
                    var jsonData = await File.ReadAllTextAsync(filePath);
                    var entities = JsonSerializer.Deserialize<List<T>>(jsonData);
                    if (entities is not null && entities.Count > 0)
                    {
                        await dbSet.AddRangeAsync(entities);
                        await dbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
