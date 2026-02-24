using GroceryStoreAPI.Models;
using System;
using System.Linq;
using Xunit;

namespace GTTest
{
    public class CustomersControllerTests
    {
        private readonly CustomerContext _context;

        public CustomersControllerTests(CustomerContext context)
        {
            _context = context;
        }

        [Fact]
        public void GetCustomers()
        {
            var customerList = _context.Customer.ToList();
            Console.WriteLine(customerList.Count);
        }

        [Fact]
        public void GetCustomerById()
        {
            int id = 1;
            var customer = _context.Customer.Find(id);

            Console.WriteLine(customer);
        }

        [Fact]
        public void AddCustomer()
        {
            var customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test last name",
                Address = "ABC Test Address",
                Email = "Test@gmail.com",
                ContactNo = "1234567890"
            };

            _context.Customer.Add(customer);
            _context.SaveChanges();
        }

        [Fact]
        public void RemoveCustomer()
        {
            int id = 1;
            var customer = _context.Customer.Find(id);

            _context.Customer.Remove(customer);
            _context.SaveChanges();
        }
    }
}