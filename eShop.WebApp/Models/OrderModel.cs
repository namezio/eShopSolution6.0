using eShop.Database;

namespace eShop.WebApp.Models;

public class OrderModel
{
    public int OrderId { set; get; }
    public int OrderUserId { get; set; }
    public float OrderAmount { get; set; }
    public string OrderPhone { get; set; }
    public string OrderAddress { get; set; }
    public string OrderName { get; set; }
    public DateTime? OrderDate { get; set; }

    public int IdUser { get; set; }
    
    public List<OrderDetailModel> OrderDetail { get; set; }
    
}