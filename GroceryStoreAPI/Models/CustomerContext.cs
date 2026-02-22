using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Models
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
    }
}