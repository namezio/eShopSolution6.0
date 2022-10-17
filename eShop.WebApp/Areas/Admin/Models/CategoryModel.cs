using eShop.Database;

namespace eShop.WebApp.Areas.Admin.Models;

public class CategoryModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public List<ProductCategory> Categories { get; set; }
}