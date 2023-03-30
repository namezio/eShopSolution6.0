using eShop.Database;
using eShop.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace eShop.API.Controllers;

[Route("[controller]")]
public class ProductController :ControllerBase
{
    private eShopEntities _entities = new eShopEntities();

    // public ProductController(eShopEntities entities)
    // {
    //     entities = _entities;
    // }

    [HttpGet("Product")]
    [Produces("application/json")]
    public JsonResult GetProduct()
    {
        var data = _entities.Products.ToList(); 
        return new JsonResult(data);
    }
    
    [HttpGet("ProductDetail/{id}")]
    [Produces("application/json")]
    public JsonResult GetProduct(int id)
    {
        var product = _entities.Products.Where(p => p.ProductId == id).FirstOrDefault();
        return new JsonResult(product);
    }

    [HttpPost("AddProduct")]
    [Produces("application/json")]
    public ActionResult AddProduct(ProductModel model)
    {
        var category = _entities.ProductCategories
            .FirstOrDefault(p => p.CategoryId == model.ProductCategoryId);
        if (category == null)
            return new JsonResult (new { error = true, message = "Your Category not found" });
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
            _entities.Products.Add(products);
            _entities.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return new JsonResult (new { error = false, message = "Create product successful !"+ model.ProductName});
    }

    [HttpPut("UpdateProduct/{id}")]
    [Produces("application/json")]
    public JsonResult UpdateProduct(int id, ProductModel model)
    {
        var products = _entities.Products.Find(id);
        if (products == null)
        {
            return new JsonResult (new { error = true, message = "Not found ProductId"});
        }
        var category = _entities.ProductCategories
            .Where (p => p.CategoryId == model.ProductCategoryId)
            .FirstOrDefault();
        if (category == null)
            return new JsonResult(new { error = true, message = "Your Category not found" });
        try
        {
            // var products = _entities.Products.Find(model.ProductId);
            products.ProductId = id;
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
            
            var z = _entities.SaveChanges();
            if ( z > 0 )
            {
                return new JsonResult(new { error = false, message = "Save Change !" + model.ProductName });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return new JsonResult(new { error = true, message = "Error" });
    }

    [HttpPut("RemoveProduct/{id}")]
    public JsonResult RemoveProduct(int id)
    {
        try
        {
            var products = _entities.Products.Find(id);
            if (products == null)
            {
                return new JsonResult(new { error = true, message = "Not Found Product ! Please check again !" });
            }
            products.ProductStatus = true;
            var z = _entities.SaveChanges();
            if ( z > 0 )
            {
                return new JsonResult(new { error = false, message = "Removed!" + products.ProductName});
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return new JsonResult(new { error = true, message = "Error !"});
    }

    [HttpDelete("DeleteProduct/{id}")]
    [Produces("application/json")]
    public JsonResult DeleteProduct(int id)
    {
        var products = _entities.Products.Find(id);
        if (products == null)
        {
            return new JsonResult(new { error = true, message = "Not Found Product ! Please check again !" });
        }
            _entities.Products.Remove(products);
            _entities.SaveChanges();
            return new JsonResult(new { error = false, message = "Delete successful !"+ id});
    }
}