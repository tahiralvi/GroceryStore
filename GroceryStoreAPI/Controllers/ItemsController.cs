using GroceryStoreAPI.Models;
using GroceryStoreAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : Controller // Changed from ControllerBase
{
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService) => _itemService = itemService;

    public async Task<IActionResult> Index()
    {
        var items = await _itemService.GetAllItemsAsync();
        return View(items);
    }
}