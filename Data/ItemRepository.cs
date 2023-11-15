using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWA.API.Properties;

namespace SWA.API.Data
{
	public class ItemRepository : IItemRepository
	{
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly AppDbContext _dbContext;

        public ItemRepository(IServiceScopeFactory serviceScopeFactory, AppDbContext dbContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _dbContext = dbContext;
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _dbContext.Items.ToList();
        }

        public void CreateItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            _dbContext.Items.Add(item);
            _dbContext.SaveChanges();
        }

        private int CalculateFactorial(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }
            return n * CalculateFactorial(n - 1);
        }

        public async Task ProcessItemConcurrently(Item item)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var factorial = CalculateFactorial(item.RowNumber);

            item.FactorialResult = factorial;
            await UpdateItemAsync(dbContext, item);
        }

        private async Task UpdateItemAsync(AppDbContext dbContext, Item item)
        {
            var existingItem = await dbContext.Items.FirstOrDefaultAsync(i => i.Id == item.Id);
            if (existingItem != null)
            {
                existingItem.FactorialResult = item.FactorialResult;
                existingItem.RowNumber = item.RowNumber;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

