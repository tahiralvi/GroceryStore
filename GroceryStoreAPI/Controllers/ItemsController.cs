using GroceryStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly GroceryContext _context;

    public ItemsController(GroceryContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems() => await _context.Items.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItems), new { id = item.ItemId }, item);
    }

    // Add Put and Delete following your Customer pattern
}