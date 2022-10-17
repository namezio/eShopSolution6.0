using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class OrderDetail
    {
        public int DetailId { get; set; }
        public int? DetailOrderId { get; set; }
        public int DetailProductId { get; set; }
        public string DetailName { get; set; }
        public float DetailPrice { get; set; }
        public int DetailQuantily { get; set; }

        public virtual Order DetailOrder { get; set; }
        public virtual Product DetailProduct { get; set; }
    }
}
