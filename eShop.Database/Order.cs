using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int OrderUserId { get; set; }
        public float OrderAmount { get; set; }
        public string OrderPhone { get; set; }
        public string OrderAddress { get; set; }
        public string OrderName { get; set; }
        public DateTime? OrderDate { get; set; }

        public virtual User OrderUser { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
