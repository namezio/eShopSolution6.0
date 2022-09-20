using System;

namespace eShop.Data.Entities
{
    public class SystemActivity
    {
        public int Id { set; get; }
        public string ActionName { set; get; }
        public DateTime ActionDate { set; get; }
        public int UserId { set; get; }
        public int ClientIP { set; get; }
    }
}