using eShop.Database;

using Microsoft.AspNetCore.Mvc;

namespace eShop.API.Controllers;

public class CategoryController :ControllerBase
{
    private eShopEntities _entities = new eShopEntities();

    // public CategoryController(eShopEntities entities)
    // {
    //     entities = _entities;
    // }
    
    [HttpGet("Category")]
    public JsonResult GetCategory()
    {
        var data = _entities.ProductCategories.ToList(); 
        return new JsonResult(data);
    }
    
    [HttpGet("Category/{id}")]
    public JsonResult GetCategory(int id)
    {
        var category = _entities.ProductCategories.Where(p => p.CategoryId == id).FirstOrDefault();
        return new JsonResult(category);
    }

    [HttpPost("AddCategory")]
    public JsonResult CreateCategory(string categoryName)
    {
        try
        {
            var category = new ProductCategory()
            {
                CategoryName = categoryName
            };
                
            _entities.ProductCategories.Add(category);
            _entities.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return new JsonResult (new { error = false, message = "Create categories successful !"+ categoryName});
    }

    [HttpPut("UpdateCategory/{id}")]
    public JsonResult UpdateCategory(int id, string categoryName)
    {
        try
        {
            var category = _entities.ProductCategories.Find(id);
            if (category == null)
            {
                return new JsonResult(new { error = true, message = "CategoryID not Found ! Plese check again !"});
            }
            category.CategoryName = categoryName;

            var z = _entities.SaveChanges();
            if ( z > 0 )
            {
                return new JsonResult (new { error = false, message = "Update categories successful !"+ categoryName});
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return new JsonResult (new { error = true, message = "Error !"});
    }

    [HttpPut("RemoveCategory/{id}")]
    public JsonResult RemoveCategory(int id)
    {
        try
        {
            var cate = _entities.ProductCategories.Find(id);
            if (cate == null)
            {
                return new JsonResult(new { error = true, message = "CategoryID not Found ! Plese check again !"});
            }
            cate.CategoryStatus = true;
            var z = _entities.SaveChanges();
            if ( z > 0 )
            {
                return new JsonResult (new { error = false, message = "Remove categories successful !"+id});
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return new JsonResult (new { error = true, message = "Error !"});
    }
    
    [HttpDelete("DeleteCategory/{id}")]
    public JsonResult DeleteCategory(int id)
    {
        var category = _entities.ProductCategories.Find(id);
        if (category == null)
        {
            return new JsonResult(new { error = true, message = "CategoryID not Found ! Plese check again !"});
        }
        _entities.ProductCategories.Remove(category);
        _entities.SaveChanges();
        return new JsonResult(new { error = false, message = "Delete successful !"+ id});
    }
}