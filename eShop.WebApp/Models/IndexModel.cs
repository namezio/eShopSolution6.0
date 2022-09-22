using eShop.Database;

namespace eShop.WebApp.Models;

public class IndexModel
{
    public List<Product> Products { get; set; }
    public List<ProductCategory> Categories { get; set; }
}