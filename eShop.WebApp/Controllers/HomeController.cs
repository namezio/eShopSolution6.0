using System.Diagnostics;
using eShop.Database;
using Microsoft.AspNetCore.Mvc;
using eShop.WebApp.Models;


namespace eShop.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    eShopEntities eShop = new eShopEntities();
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var products = eShop.Products.ToList();
        var categories = eShop.ProductCategories.ToList();
        IndexModel model = new IndexModel();
        model.Categories = categories;
        model.Products = products;
        return View(model);
    }

    public ActionResult OrderDetail()
    {
        return View();
    }
    // public ActionResult AddToDetail(int Id, int Quantity)
    // {
    //     if (Session["orderDetails"] == null)
    //     {
    //         List<OrderDetailModel> detailModels = new List<OrderDetailModel>();
    //         detailModels.Add(new OrderDetailModel(){Product = eShop.Products.Find(Id), Quantity = Quantity});
    //         Session["orderDetails"] = detailModels;
    //         HttpContext.
    //         Session["count"] = 1;
    //     }
    //     else
    //     {
    //         List<OrderDetailModel> detailModels = (List<OrderDetailModel>)Session["orderDetail"];
    //     }
    // }
    public IActionResult Product()
    {
        var products = eShop.Products.ToList();
        var categories = eShop.ProductCategories.ToList();
        IndexModel model = new IndexModel();
        model.Categories = categories;
        model.Products = products;
        return View(model);
    }
    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Login(string email, string password)
    {
        var Useremail = email;
        var UserPass = password;
        var acc = eShop.Users.SingleOrDefault(x => x.UserEmail == Useremail && x.UserPassword == UserPass);
        if (acc!= null)
        {
            HttpContext.Session.GetString();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Insert is successfull')", true);
            return RedirectToAction("OrderDetail", "Home");
        }
        else
        {
            return View();
        }
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
