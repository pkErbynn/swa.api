using System;
using System.Runtime.InteropServices;
using SWA.API.Properties;

namespace SWA.API.Data
{
    public interface IItemRepository
	{
        IEnumerable<Item> GetAllItems();
        void CreateItem(Item platform);
        Task ProcessItemConcurrently(Item item);
    }
}

