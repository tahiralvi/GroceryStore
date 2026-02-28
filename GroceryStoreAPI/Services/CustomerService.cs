using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly GroceryContext _context;
        public CustomerService(GroceryContext context) => _context = context;

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _context.Customer.ToListAsync();
        public async Task<Customer> GetByIdAsync(int id) => await _context.Customer.FindAsync(id);
        public async Task CreateAsync(Customer customer) { _context.Add(customer); await _context.SaveChangesAsync(); }
        public async Task UpdateAsync(Customer customer) { _context.Update(customer); await _context.SaveChangesAsync(); }
        public async Task DeleteAsync(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer != null) { _context.Customer.Remove(customer); await _context.SaveChangesAsync(); }
        }
    }
}
