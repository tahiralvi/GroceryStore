using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class OrdersController : ControllerBase
{
    private readonly GroceryContext _context;

    public OrdersController(GroceryContext context) => _context = context;

    // Place a new order
    [HttpPost]
    public async Task<ActionResult<Order>> PlaceOrder(Order order)
    {
        // 1. Basic Validation
        if (order.OrderDetails == null || !order.OrderDetails.Any())
            return BadRequest("Order must have at least one item.");

        // 2. Process stock and calculate total (Simplified for this example)
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderDetails)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) return NotFound();
        return order;
    }
}