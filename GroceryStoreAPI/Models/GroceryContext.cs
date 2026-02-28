using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Models
{
    public class GroceryContext : DbContext
    {
        public GroceryContext(DbContextOptions<GroceryContext> options) : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
    }
}