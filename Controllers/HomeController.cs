using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project.Areas.Identity.Data;
using project.Models;
using project.Repository;
using System.Diagnostics;

namespace project.Controllers
{
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;

      
        private ProductDAO productDAO= new ProductDAO();
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger , IProductRepository productRepository, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _productRepository = productRepository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            List<Product> ds = productDAO.GetAllProducts();
            return View(ds);
        }

        public IActionResult Cart()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Single( int id)
        {
         return View(_productRepository.GetProductById(id));
        }
        public IActionResult Checkout()
        {
            return View();
        }
        public IActionResult Succes()
        {
            return View();
        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/home");
        }
        public IActionResult findProductByCart(int id)

        {
            List<Product> products= _productRepository.GetAllProByCartId(id);
            return View(products);
        }
        public IActionResult Search(string productName)
        {
            List<Product> products = _productRepository.searchProductByName(productName);
            return View(products);
        }
    }
}