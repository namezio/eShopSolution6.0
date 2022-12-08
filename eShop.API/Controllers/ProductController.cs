using eShop.Database;
using Microsoft.AspNetCore.Mvc;

namespace eShop.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController :ControllerBase
{
    private eShopEntities _entities = new eShopEntities();

    public ProductController(eShopEntities entities)
    {
        entities = _entities;
    }

    [HttpGet]
    public JsonResult GetProduct()
    {
        var data = _entities.Products.ToList(); 
        return new JsonResult(data);
    }
    
    [HttpGet("{id}")]
    public JsonResult GetProduct(int id)
    {
        var product = _entities.Products.Where(p => p.ProductId == id).FirstOrDefault();
        return new JsonResult(product);
    }
}