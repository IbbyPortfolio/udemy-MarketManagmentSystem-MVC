using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
public class CategoriesController : Controller
{
    public IActionResult Index()
    {
        var categories = CategoriesInMemoryRepository.GetCategories();
        return View(categories);
    }

    public IActionResult Edit(int? id)
    {
        ViewBag.Action = "edit";

        var category = CategoriesInMemoryRepository.GetCategoryById(id.HasValue ? id.Value : 0);
        return View(category);

    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            CategoriesInMemoryRepository.UpdateCategory(category.CategoryId, category);
            return RedirectToAction(nameof(Index));
        }
        return View(category);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "add";
        return View();
    }

    [HttpPost]
    public IActionResult Add([FromForm] Category category)
    {
        if (ModelState.IsValid)
        {
            CategoriesInMemoryRepository.AddCategory(category);
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        CategoriesInMemoryRepository.DeleteCategory(id);
        return RedirectToAction(nameof(Index));
    }



}