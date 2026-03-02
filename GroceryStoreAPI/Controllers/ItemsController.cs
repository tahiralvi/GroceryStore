using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ItemsController : Controller // Changed from ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService) => _itemService = itemService;

    public async Task<IActionResult> Index()
    {
        var items = await _itemService.GetAllItemsAsync();
        return View(items);
    }
    // GET: api/Items/Create
    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View();
    }

    // POST: api/Items/Create
    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description,Price,StockQuantity")] Item item)
    {
        if (ModelState.IsValid)
        {
            await _itemService.AddItemAsync(item);
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // GET: Items/Details/5
    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var item = await _itemService.GetItemByIdAsync(id.Value);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }
}