using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class OptionGroup
    {
        public OptionGroup()
        {
            ProductOptions = new HashSet<ProductOption>();
        }

        public int OptionGroupId { get; set; }
        public string OptionGroupName { get; set; }

        public virtual ICollection<ProductOption> ProductOptions { get; set; }
    }
}
