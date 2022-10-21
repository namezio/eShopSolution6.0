using eShop.Database;

namespace eShop.WebApp.Areas.Admin.Models;

public class UserModel
{
    public int UserId { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserPhone { get; set; }
    public string UserAddress { get; set; }
    public List<User> Users { get; set; }
}