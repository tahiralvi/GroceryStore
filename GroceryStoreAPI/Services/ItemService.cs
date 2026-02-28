using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services
{
    public class ItemService : IItemService
    {
        private readonly GroceryContext _context; //
        public ItemService(GroceryContext context) => _context = context;

        public async Task<IEnumerable<Item>> GetAllItemsAsync() => await _context.Items.ToListAsync();
        public async Task<Item> GetItemByIdAsync(int id) => await _context.Items.FindAsync(id);
        public async Task AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public Task UpdateItemAsync(Item item)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteItemAsync(int id)
        {
            throw new System.NotImplementedException();
        }
        // Implement Update and Delete similarly...
    }
}
