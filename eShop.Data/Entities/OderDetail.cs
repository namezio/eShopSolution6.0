using System.Collections.Generic;

namespace eShop.Data.Entities
{
    public class OderDetail
    {
        public int OderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
        
        public Product Product { get; set; }
        
    }
}