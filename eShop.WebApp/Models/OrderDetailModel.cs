using eShop.Database;

namespace eShop.WebApp.Models;

public class OrderDetailModel
{
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}