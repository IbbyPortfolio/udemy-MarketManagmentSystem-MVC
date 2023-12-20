using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Features;
public class ProductsController : Controller
{
 
    public IActionResult Index()
    {
        List<Product> products= ProductsRepository.GetProducts(loadCategory:true);
        return View(products);
    }

    public IActionResult Edit(int id)
    {

        var productViewModel = new ProductViewModel
        {
            Product = ProductsRepository.GetProductById(id)?? new Product(),
            Categories = CategoriesInMemoryRepository.GetCategories()
        };
        
        ViewBag.Action = "edit";
        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            ProductsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }
        // re-fill the categrios list
        ViewBag.Action = "edit";
        productViewModel.Categories= CategoriesInMemoryRepository.GetCategories();
        return View(productViewModel);
    }

    public IActionResult Add()
    {
        ProductViewModel viewModel = new ProductViewModel()
        {
            Categories = CategoriesInMemoryRepository.GetCategories()
        };
        ViewBag.Action = "add";
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult Add([FromForm] ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            ProductsRepository.AddProduct(productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "add";
        productViewModel.Categories = CategoriesInMemoryRepository.GetCategories();
        return View(productViewModel);
    }

    public IActionResult Delete(int id)
    {
        ProductsRepository.DeleteProduct(id);
       
        return RedirectToAction(nameof(Index));


    }

    public IActionResult ProductByCategoryPartial(int categoryId)
    {
        var products = ProductsRepository.GetProductByCategoryId(categoryId);

        return PartialView("_Products",products);
    }
}
