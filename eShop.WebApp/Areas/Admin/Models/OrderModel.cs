using eShop.Database;

namespace eShop.WebApp.Areas.Admin.Models;

public class OrderModel
{
    public int OrderId { get; set; }
    public int OrderUserId { get; set; }
    public float OrderAmount { get; set; }
    public string OrderPhone { get; set; }
    public string OrderAddress { get; set; }
    public string OrderName { get; set; }
    public DateTime? OrderDate { get; set; }
    public List<Order> Orders { get; set; }
}