using System.Diagnostics;
using System.Text.Json.Serialization;
using eShop.Database;
using eShop.Library.Extensions;
using Microsoft.AspNetCore.Mvc;
using eShop.WebApp.Models;
using eShop.WebApp.Repository;
using MySqlX.XDevAPI;
using Newtonsoft.Json;


namespace eShop.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISessionManager _sessionManager;
    
    public HomeController(ISessionManager sessionManager)
    {
        _sessionManager = sessionManager;
    }
    
    eShopEntities eShop = new eShopEntities();
    public const string CARTKEY = "cart";
    public const string LOGINKEY = "login";

    public AccountLoginModel GetEmail()
    {
        return _sessionManager.GetValue<AccountLoginModel>(LOGINKEY);
    }


    public AccountLoginModel GetLoginSession()
    {
        var json = HttpContext.Session.GetString(LOGINKEY);
        if (string.IsNullOrEmpty(json))
            return null;

        return JsonConvert.DeserializeObject<AccountLoginModel>(json);
    }
    
    
    public void SaveLoginSession(AccountLoginModel ml)
    {
        _sessionManager.SetValue(LOGINKEY, ml);
    }

    // Lấy cart từ Session (danh sách CartItem)
    public List<OrderDetailModel> GetCartItems()
    {
        return _sessionManager.GetValue<List<OrderDetailModel>>(CARTKEY);
    }
    

    // Xóa cart khỏi session
    public void ClearCart()
    {
        _sessionManager.Remove(CARTKEY);
    }

    // Lưu Cart (Danh sách CartItem) vào session
    public void SaveCartSession(List<OrderDetailModel> ls)
    {
        _sessionManager.SetValue(CARTKEY, ls);
    }
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
        return View(GetCartItems());
    }
    public IActionResult AddToCart(int id, ISessionManager sessionManager)
    {
        var product = eShop.Products
            .Where(p => p.ProductId == id)
            .FirstOrDefault();
        if (product == null)
            return NotFound("Không có sản phẩm");

        // Xử lý đưa vào Cart ...
        var cart = sessionManager.GetValue<List<OrderDetailModel>>(CARTKEY);
        var cartitem = cart.Find(p => p.Product.ProductId == id);
        if (cartitem != null)
        {
            // Đã tồn tại, tăng thêm 1
            cartitem.Quantity++;
        }
        else
        {
            //  Thêm mới
            cart.Add(new OrderDetailModel() { Quantity = 1, Product = product });
        }

        // Lưu cart vào Session
        sessionManager.SetValue(CARTKEY, cart);

        // Chuyển đến trang hiện thị Cart
        return RedirectToAction("OrderDetail", "Home");
    }
    
    [HttpPost]
    public IActionResult UpdateCart ( int productid, int quantity) {
        // Cập nhật Cart thay đổi số lượng quantity ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Product.ProductId == productid);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cartitem.Quantity = quantity;
            if (quantity == 0)
            {
                cart.Remove(cartitem);
            }
            else if (quantity < 0)
            {
                return Json(new { error = true, message = "Quantity must >0" });
            }
            else if (quantity>100)
            {
                return Json(new { error = true, message = "Quantity must <100" });
            }
            
        }
        SaveCartSession (cart);
        return Json(new {error = false, message = ""});
    }
    
    
    public ActionResult RemoveCart (int productid) {
        try
        {
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.Product.ProductId == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
                SaveCartSession(cart);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Json(new {error = false, message = ""});
    }
    public IActionResult Product()
    {
        var products = eShop.Products.ToList();
        IndexModel model = new IndexModel();
        model.Products = products;
        return View(model);
    }

    public ActionResult DetailProduct(int id)
    {
        Product? product = eShop.Products.Find(id);
        return View(product);
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public ActionResult GetLogin(string email, string password)
    {
        var Useremail = email;
        var UserPass = password.Trim().Md5();
        var acc = eShop.Users.SingleOrDefault(x => x.UserEmail == Useremail && x.UserPassword == UserPass);
        if (acc == null)
        {
            // return RedirectToAction("Checkout", "Home");
            return Json(new { error = true, message = "Wrong email or password, plese check again !" });
        }
        var userid = acc.UserId;
        SaveLoginSession(new AccountLoginModel() {id = userid});
        return Json(new { error = false, message = "" });
    }

    public IActionResult GoToCheckOut()
    {
        var session = HttpContext.Session;
        string jsonEmail = session.GetString (LOGINKEY);
        if (jsonEmail == null)
        {
            return RedirectToAction("Login", "Home");
        }
        return RedirectToAction("Checkout", "Home");
        
    }
    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = GetCartItems();
        var model = new OrderModel
        {
            OrderDetail = cart,
            OrderAmount = cart.Sum(x=>x.Quantity*x.Product.ProductPrice)
        };
        
        return View(model);
    }
    [HttpPost]
    public IActionResult Checkout(OrderModel model)
    {
        var id = GetLoginSession().id;
        var cart = GetCartItems();
        var user = eShop.Users.Where(p => p.UserId == model.IdUser).FirstOrDefault();
        try
        {
            var order = new Order()
            {
                OrderUserId = id,
                OrderAddress = model.OrderAddress,
                OrderName = model.OrderName,
                OrderAmount = model.OrderAmount,
                OrderDate = DateTime.Now,
                OrderPhone = model.OrderPhone
            };
            foreach (var a in cart)
            {
                var orderDetail = new OrderDetail()
                {
                    DetailProductId = a.Product.ProductId,
                    DetailName = a.Product.ProductName,
                    DetailPrice = a.Product.ProductPrice,
                    DetailQuantily = a.Quantity
                };
                order.OrderDetails.Add(orderDetail);
            }
            eShop.Orders.Add(order);
            eShop.SaveChanges();
            ClearCart();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("CheckoutSuccess", "Home");
    }

    public IActionResult CheckoutSuccess()
    {
        var order = eShop.Orders.ToList();
        OrderSuccessModel model = new OrderSuccessModel();
        model.OrderModels = order;
        return View(model);
    }

    public IActionResult GetAccount()
    {
        var session = HttpContext.Session;
        string jsonEmail = session.GetString (LOGINKEY);
        if (jsonEmail == null)
        {
            return RedirectToAction("Login", "Home");
        }
        return RedirectToAction("Account", "Home");
    }

    public IActionResult Account()
    {
        var id = GetLoginSession().id;
        User? user = eShop.Users.Find(id);
        return View(user);
    }

    public ActionResult SignUp()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult SignUp(SignUpModel model)
    {
        var email = eShop.Users.SingleOrDefault(x => x.UserEmail == model.UserEmail);
        if (email != null)
        {
            return Json(new { error = true, message = "This email has been registered, please chose another email !" });
        }
        try
        {
            var users = new User()
            {
                UserEmail = model.UserEmail,
                UserPassword = model.UserPassword.Md5(),
                UserPhone = model.UserPhone,
                UserAddress = model.UserAddress,
                UserFirstName = model.UserFirstName,
                UserLastName = model.UserLastName,
            };
            eShop.Users.Add(users);
            eShop.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("Login", "Home");
    }

    public ActionResult LogOut()
    {
        var session = HttpContext.Session;
        session.Remove(LOGINKEY);
        return RedirectToAction("Index");
    }
    
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

