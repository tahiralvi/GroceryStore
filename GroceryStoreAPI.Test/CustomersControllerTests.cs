using GroceryStoreAPI.Controllers;
using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroceryStoreAPI.Test
{
    public class CustomersControllerTests
    {
        private CustomerContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<CustomerContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new CustomerContext(options);
        }

        [Fact]
        public async Task GetCustomers_ReturnsAllCustomers()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                context.Customer.AddRange(
                    new Customer
                    {
                        CustomerId = 1,
                        FirstName = "Alice",
                        LastName = "Smith",
                        Address = "Address 1",
                        Email = "alice@example.com",
                        ContactNo = "1111111111"
                    },
                    new Customer
                    {
                        CustomerId = 2,
                        FirstName = "Bob",
                        LastName = "Jones",
                        Address = "Address 2",
                        Email = "bob@example.com",
                        ContactNo = "2222222222"
                    });
                context.SaveChanges();

                var controller = new CustomersController(context);
                var actionResult = await controller.GetAllCustomers();
                var customers = actionResult.Value;

                Assert.NotNull(customers);
                Assert.Equal(2, customers.Count());
            }
        }

        [Fact]
        public async Task GetCustomerById_ReturnsCustomer()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                context.Customer.Add(new Customer
                {
                    CustomerId = 1,
                    FirstName = "Test",
                    LastName = "User",
                    Address = "Test Address",
                    Email = "test@example.com",
                    ContactNo = "1234567890"
                });
                context.SaveChanges();

                var controller = new CustomersController(context);
                var actionResult = await controller.GetSingleCustomer(1);
                var customer = actionResult.Value;

                Assert.NotNull(customer);
                Assert.Equal("Test", customer.FirstName);
            }
        }

        [Fact]
        public async Task AddCustomer_CreatesCustomer()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                var controller = new CustomersController(context);

                var newCustomer = new Customer
                {
                    FirstName = "New",
                    LastName = "Customer",
                    Address = "New Address",
                    Email = "new@example.com",
                    ContactNo = "9876543210"
                };

                var actionResult = await controller.AddCustomer(newCustomer);

                // PostCustomer returns ActionResult<Customer> with Result being CreatedAtActionResult
                Assert.IsType<CreatedAtActionResult>(actionResult.Result);

                var created = (actionResult.Result as CreatedAtActionResult)?.Value as Customer;
                Assert.NotNull(created);
                Assert.Equal("New", created.FirstName);

                // Ensure it's persisted in the context
                Assert.True(context.Customer.Any(c => c.Email == "new@example.com"));
            }
        }

        [Fact]
        public async Task RemoveCustomer_DeletesCustomer()
        {
            var dbName = Guid.NewGuid().ToString();
            using (var context = CreateInMemoryContext(dbName))
            {
                context.Customer.Add(new Customer
                {
                    CustomerId = 1,
                    FirstName = "ToDelete",
                    LastName = "User",
                    Address = "Delete Address",
                    Email = "delete@example.com",
                    ContactNo = "0000000000"
                });
                context.SaveChanges();

                var controller = new CustomersController(context);
                var deleteResult = await controller.DeleteCustomer(1);

                // DeleteCustomer returns the deleted customer as Value on success
                Assert.NotNull(deleteResult.Value);
                Assert.Equal("ToDelete", deleteResult.Value.FirstName);

                // Verify removal from store
                Assert.Null(context.Customer.Find(1));
            }
        }
    }
}