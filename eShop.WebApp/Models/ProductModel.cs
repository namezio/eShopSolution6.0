using eShop.Database;

namespace eShop.WebApp.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public float ProductPrice { get; set; }
    public string ProductLongDesc { get; set; }
    public string ProductImage { get; set; }
    public int ProductCategoryId { get; set; }
    
}