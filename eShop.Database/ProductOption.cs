using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class ProductOption
    {
        public int ProductOptionId { get; set; }
        public int OptionId { get; set; }
        public int ProductId { get; set; }
        public int OptionGroupId { get; set; }
        public double? OptionPriceIncrement { get; set; }

        public virtual Option Option { get; set; }
        public virtual OptionGroup OptionGroup { get; set; }
        public virtual Product Product { get; set; }
    }
}
