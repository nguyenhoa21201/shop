using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using project.Areas.Identity.Data;
using project.Models;
using project.Repository;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ICategoryRepository categoryRepository, SignInManager<ApplicationUser> signInManager)
        {
            _categoryRepository = categoryRepository;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllCategories()
        {
            //1. get data
            List<Category> lst = _categoryRepository.GetAll();
            //2 passign data to view
            return View("ViewAllCategories", lst);
        }
        [HttpPost]
        public IActionResult saveCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                //check name in db
                bool isCategoryNameExist = _categoryRepository.checkName(category.CategoryName);
                if (isCategoryNameExist)
                {
                    ModelState.AddModelError(string.Empty, "CategorName is Exist");
                    return View("CreateCategory");
                }
                else
                {
                    _categoryRepository.Create(category);
                    return RedirectToAction("ViewAllCategories");
                }


            }
            else
            {
                return View("CreateCate");
            }

        }
        public IActionResult CreateCategory()
        {

            return View("CreateCategory", new Category());
        }
        public IActionResult EditCategory(int id)
        {

            return View("EditCategory", _categoryRepository.findById(id));
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {

            _categoryRepository.Update(category);
            return RedirectToAction("ViewAllCategories");
        }
        public IActionResult DeleteCategory(int id)
        {

            _categoryRepository.Delete(id);
            return RedirectToAction("ViewAllCategories");

        }
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return LocalRedirect("/identity/account/login");
        }
    }
}
