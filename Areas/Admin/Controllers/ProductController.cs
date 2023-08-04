using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project.Models;

using project.Repository;
using System.Data;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;    
        private IBrandRepository _brandRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllProducts()
        {
            //1. get data
            List<Product> lst = _productRepository.GetAll();
            //2 passign data to view
            return View("ViewAllProducts", lst);
        }
        [HttpPost]
        public IActionResult saveProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                //check name in db
                bool isProductNameExist = _productRepository.checkName(product.ProductName);
                if (isProductNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Product is Exist");
                    return View("CreateProduct");
                }
                else
                {
                    _productRepository.Create(product);
                    return RedirectToAction("ViewAllProducts");
                }


            }
            else
            {
                return View("CreateProduct");
            }

        }
       
        
        public IActionResult EditProduct(int id)
        {

            var q1 = from c in _categoryRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.CategoryName,
                         Value = c.CategoryId.ToString(),
                     };
            var q2 = from c in _brandRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.BrandName,
                         Value = c.BrandId.ToString(),
                     };
            ViewBag.CategoryId = q1.ToList();
            ViewBag.BrandId = q2.ToList();

            return View("EditProduct", _productRepository.GetProductById(id));
        }
        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {

            _productRepository.UpdateProduct(product);
            return RedirectToAction("ViewAllProducts");
        }
        public IActionResult DeleteProduct(int id)
        {

            _productRepository.DeleteProduct(id);
            return RedirectToAction("ViewAllProducts");

        }
        [HttpGet]
        public IActionResult CreateProduct()
        {

            var q1 = from c in _categoryRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.CategoryName,
                         Value = c.CategoryId.ToString(),
                     };
            var q2 = from c in _brandRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.BrandName,
                         Value = c.BrandId.ToString(),
                     };
            ViewBag.CategoryId = q1.ToList();
            ViewBag.BrandId = q2.ToList();

            return View("CreateProduct", new Product());
        }
    }

}

