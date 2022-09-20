using System;
using System.Collections.Generic;
using eShop.Data.Enum;

namespace eShop.Data.Entities
{
    public class Order
    {
        public int Id { set; get; }
        public DateTime OderDate { set; get; }
        public Guid UserId { set; get; }
        public string ShipName { set; get; }
        public string ShipAddress { set; get; }
        public string ShipEmail { set; get; }
        public string ShipPhoneNumber { set; get; }
        public OrderStatus Status { set; get; }
        public List<OderDetail> OderDetails { get; set; }
    }
}