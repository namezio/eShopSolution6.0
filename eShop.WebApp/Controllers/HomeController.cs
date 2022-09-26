using System.Diagnostics;
using System.Text.Json.Serialization;
using eShop.Database;
using Microsoft.AspNetCore.Mvc;
using eShop.WebApp.Models;
using MySqlX.XDevAPI;
using Newtonsoft.Json;


namespace eShop.WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    eShopEntities eShop = new eShopEntities();
    public const string CARTKEY = "cart";

    // Lấy cart từ Session (danh sách CartItem)
    public List<OrderDetailModel> GetCartItems() {

        var session = HttpContext.Session;
        string jsoncart = session.GetString (CARTKEY);
        if (jsoncart != null) {
            return JsonConvert.DeserializeObject<List<OrderDetailModel>> (jsoncart);
        }
        return new List<OrderDetailModel> ();
    }

    // Xóa cart khỏi session
    void ClearCart () {
        var session = HttpContext.Session;
        session.Remove (CARTKEY);
    }

    // Lưu Cart (Danh sách CartItem) vào session
    void SaveCartSession (List<OrderDetailModel> ls) {
        var session = HttpContext.Session;
        string jsoncart = JsonConvert.SerializeObject (ls);
        session.SetString (CARTKEY, jsoncart);
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
    // [HttpPost]
    // public ActionResult Index(int productId)
    // {
    //     return View();
    // }
    public ActionResult OrderDetail()
    {
        return View(GetCartItems());
    }
    public IActionResult AddToCart (int id) {

        var product = eShop.Products
            .Where (p => p.ProductId == id)
            .FirstOrDefault();
        if (product == null)
            return NotFound ("Không có sản phẩm");

        // Xử lý đưa vào Cart ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Product.ProductId == id);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cartitem.Quantity++;
        } else {
            //  Thêm mới
            cart.Add (new OrderDetailModel() { Quantity  = 1, Product = product });
        }

        // Lưu cart vào Session
        SaveCartSession (cart);
        // Chuyển đến trang hiện thị Cart
        return RedirectToAction("OrderDetail","Home");
    }
    
    [HttpPost]
    public IActionResult UpdateCart ([FromForm] int id, [FromForm] int quantity) {
        // Cập nhật Cart thay đổi số lượng quantity ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Product.ProductId == id);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cartitem.Quantity = quantity;
        }
        SaveCartSession (cart);
        // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
        return Ok();
    }
    
    public IActionResult RemoveCart (int id) {
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.Product.ProductId == id);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cart.Remove(cartitem);
        }
        SaveCartSession (cart);
        return RedirectToAction ("OrderDetail","Home");
    }
    public IActionResult Product()
    {
        var products = eShop.Products.ToList();
        IndexModel model = new IndexModel();
        model.Products = products;
        return View(model);
    }

    public ActionResult CreateProduct()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CreateProduct(ProductModel model)
    {
        try
        {
            var products = new Product()
            {
                ProductName = model.ProductName,
                ProductPrice = model.ProductPrice,
                ProductCategoryId = model.ProductCategoryId,
                ProductImage = model.ProductImage,
                ProductLongDesc = model.ProductLongDesc,
            };
            eShop.Products.Add(products);
            eShop.SaveChanges();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("Product", "Home");
    }

    public ActionResult DetailProduct(int id)
    {
        Product? product = eShop.Products.Find(id);
        return View(product);
    }
    [HttpGet]
    public ActionResult UpdateProduct(int id)
    {
        Product? productModel = eShop.Products.Find(id);
        return View(productModel);
    }
    [HttpPost]
    public ActionResult UpdateProduct(ProductModel model)
    {
        try
        {
            var products = eShop.Products.Find(model.ProductId);
            products.ProductName = model.ProductName;
            products.ProductImage = model.ProductImage;
            products.ProductLongDesc = model.ProductLongDesc;
            products.ProductCategoryId = model.ProductCategoryId;
            products.ProductPrice = model.ProductPrice;
            // eShop.Products.Update(products);
            
            var z = eShop.SaveChanges();
            if ( z > 0 )
            {
                return RedirectToAction("Product","Home");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("Product", "Home");
    }

    public ActionResult DeteleProduct(int id)
    {
        var model = eShop.Products.Find(id);
        eShop.Products.Remove(model);
        eShop.SaveChanges();
        return RedirectToAction("Product", "Home");
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
        UserModels models = new UserModels();
        
        if (acc!= null)
        {
            HttpContext.Session.SetString("email", Useremail);
            return RedirectToAction("OrderDetail", "Home");
        }
        else
        {
            return View();
        }
    }

    public ActionResult Category()
    {
        var categories = eShop.ProductCategories.ToList();
        IndexModel model = new IndexModel();
        model.Categories = categories;
        return View(model);
    }

    public ActionResult CreateCategory()
    {
        return View();
    }
    [HttpPost]
    public ActionResult CreateCategory(CategoryModel model)
    {
        
            try
            {
                var category = new ProductCategory()
                {
                    CategoryName = model.CategoryName
                };
                
                eShop.ProductCategories.Add(category);
                eShop.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Category", "Home");
        
    }
    [HttpGet]
    [HttpGet]
    public ActionResult UpdateCategory(int id)
    {
        ProductCategory? categoryModel = eShop.ProductCategories.Find(id);
        return View(categoryModel);
    }
    [HttpPost]
    public ActionResult UpdateCategory(CategoryModel model)
    {
        try
        {
            var category = eShop.ProductCategories.Find(model.CategoryId);
            category.CategoryName = model.CategoryName;
            // eShop.Products.Update(products);
            
            var z = eShop.SaveChanges();
            if ( z > 0 )
            {
                return RedirectToAction("Category","Home");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return RedirectToAction("Category", "Home");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    public ActionResult DeleteCategory(int id)
    {
        var model = eShop.ProductCategories.Find(id);
        eShop.ProductCategories.Remove(model);
        eShop.SaveChanges();
        return RedirectToAction("Category", "Home");
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

