using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class Option
    {
        public Option()
        {
            ProductOptions = new HashSet<ProductOption>();
        }

        public int OptionId { get; set; }
        public string OptionName { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
