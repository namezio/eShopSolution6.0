using eShop.Database;
using eShop.WebApp.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
namespace eShop.WebApp.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    eShopEntities eShop = new eShopEntities();

    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Product()
    {
        var products = eShop.Products.ToList();
        ProductModel model = new ProductModel();
        model.Product = products;
        return View(model);
    }

    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateProduct(ProductModel model)
    {
        var category = eShop.ProductCategories
            .Where(p => p.CategoryId == model.ProductCategoryId)
            .FirstOrDefault();
        if (category == null)
            return Json(new { error = true, message = "Your Category not found" });
        try
        {
            var products = new Product()
            {
                ProductId = model.ProductId,
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                ProductCartDesc = model.ProductCartDesc,
                ProductImage = model.ProductImage,
                ProductLive = model.ProductLive,
                ProductStock = model.ProductStock,
                ProductLongDesc = model.ProductLongDesc,
                ProductThumb = model.ProductThumb,
                ProductShortDesc = model.ProductShortDesc,
                ProductWeight = model.ProductWeight,
                ProductCategoryId = model.ProductCategoryId,
                ProductUpdateDate = model.ProductUpdateDate,
                ProductLocation = model.ProductLocation,
                ProductUnlimited = model.ProductUnlimited
            };
            eShop.Products.Add(products);
            eShop.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return RedirectToAction("Product", "Admin");
    }

    [HttpGet]
    public IActionResult UpdateProduct(int id)
    {
        var productModel = eShop.Products.Find(id);
        return View(productModel);
    }

    [HttpPost]
    public IActionResult UpdateProduct(ProductModel model)
    {
        var category = eShop.ProductCategories
            .Where (p => p.CategoryId == model.ProductCategoryId)
            .FirstOrDefault();
        if (category == null)
            return Json(new { error = true, message = "Your Category not found" });
        try
        {
            var products = eShop.Products.Find(model.ProductId);
            products.ProductName = model.ProductName;
            products.ProductPrice = model.ProductPrice;
            products.ProductCartDesc = model.ProductCartDesc;
            products.ProductImage = model.ProductImage;
            products.ProductLive = model.ProductLive;
            products.ProductStock = model.ProductStock;
            products.ProductLongDesc = model.ProductLongDesc;
            products.ProductThumb = model.ProductThumb;
            products.ProductShortDesc = model.ProductShortDesc;
            products.ProductWeight = model.ProductWeight;
            products.ProductCategoryId = model.ProductCategoryId;
            products.ProductUpdateDate = model.ProductUpdateDate;
            products.ProductLocation = model.ProductLocation;
            products.ProductUnlimited = model.ProductUnlimited;
            // eShop.Products.Update(products);
            
            var z = eShop.SaveChanges();
            if ( z > 0 )
            {
                return RedirectToAction("Product","Admin");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("Product", "Admin");
    }

    public ActionResult DeleteProduct(int id)
    {
        var model = eShop.Products.Find(id);
        eShop.Products.Remove(model);
        eShop.SaveChanges();
        return RedirectToAction("Product", "Admin");
    }
    public ActionResult Category()
    {
        var categories = eShop.ProductCategories.ToList();
        CategoryModel model = new CategoryModel();
        model.Categories = categories;
        return View(model);
    }

    public ActionResult CreateCategory()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CreateCategory(CategoryModel model)
    {
        
            try
            {
                var category = new ProductCategory()
                {
                    CategoryName = model.CategoryName
                };
                
                eShop.ProductCategories.Add(category);
                eShop.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Category", "Admin");
        
    }
    
    [HttpGet]
    public ActionResult UpdateCategory(int id)
    {
        ProductCategory? categoryModel = eShop.ProductCategories.Find(id);
        return View(categoryModel);
    }
    [HttpPost]
    public ActionResult UpdateCategory(CategoryModel model)
    {
        try
        {
            var category = eShop.ProductCategories.Find(model.CategoryId);
            category.CategoryName = model.CategoryName;

            var z = eShop.SaveChanges();
            if ( z > 0 )
            {
                return RedirectToAction("Category","Admin");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("Category", "Admin");
    }
    
    [HttpGet]
    public ActionResult DeleteCategory(int id)
    {
        var model = eShop.ProductCategories.Find(id);
        eShop.ProductCategories.Remove(model);
        eShop.SaveChanges();
        return RedirectToAction("Category", "Admin");
    }
}