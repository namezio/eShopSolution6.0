using System;
using System.Collections.Generic;

#nullable disable

namespace eShop.Database
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
