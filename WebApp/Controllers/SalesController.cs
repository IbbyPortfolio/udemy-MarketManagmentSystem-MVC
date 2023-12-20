using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers;
public class SalesController : Controller
{
    public IActionResult Index()
    {
        SalesViewModel salesViewModel = new SalesViewModel()
        {
            Categories = CategoriesInMemoryRepository.GetCategories()
           
        };
        return View(salesViewModel);
    }
}
