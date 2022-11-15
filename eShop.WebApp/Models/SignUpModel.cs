namespace eShop.WebApp.Models;

public class SignUpModel
{
    public int UserId { get; set; }
    public string UserEmail { get; set; }
    public string UserPassword { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserPhone { get; set; }
    public string UserAddress { get; set; }
}