using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class ItemsController : Controller // Changed from ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService) => _itemService = itemService;

    [HttpGet("")]
    [HttpGet("Index")]
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

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        // Note: Using ItemId to match your Model
        var item = await _itemService.GetItemByIdAsync(id.Value);

        if (item == null) return NotFound();

        return View(item);
    }

    // GET: Items/Edit/5
    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var item = await _itemService.GetItemByIdAsync(id.Value);
        if (item == null) return NotFound();

        return View(item);
    }

    // POST: Items/Edit/5
    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ItemId,Name,Description,Price,StockQuantity")] Item item)
    {
        if (id != item.ItemId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _itemService.UpdateItemAsync(item);
            }
            catch (Exception) // Logic for DbUpdateConcurrencyException usually goes here
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }
}