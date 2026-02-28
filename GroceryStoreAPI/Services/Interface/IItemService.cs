using GroceryStoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task<Item> GetItemByIdAsync(int id);
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(int id);
    }
}
