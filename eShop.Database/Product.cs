using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
            ProductOptions = new HashSet<ProductOption>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public float ProductWeight { get; set; }
        public string ProductCartDesc { get; set; }
        public string ProductShortDesc { get; set; }
        public string ProductLongDesc { get; set; }
        public string ProductThumb { get; set; }
        public string ProductImage { get; set; }
        public int ProductCategoryId { get; set; }
        public DateTime? ProductUpdateDate { get; set; }
        public float? ProductStock { get; set; }
        public bool? ProductLive { get; set; }
        public bool? ProductUnlimited { get; set; }
        public string ProductLocation { get; set; }
        public bool ProductStatus { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
