using System;
using System.Collections.Generic;

namespace eShop.Data.Entities
{
    public class Language
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public bool IsDefault { set; get; }
        public List<ProductTranslation> ProductTranslations { get; set; }

        public List<CategoryTranslation> CategoryTranslations   { get; set; }
    }
}