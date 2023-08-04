using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Repository;
using System.Data;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]

    public class BrandController : Controller
    {
        private IBrandRepository _brandRepository;
        public BrandController(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllBrands()
        {
            List<Brand> lst =_brandRepository.GetAll();
            return View("ViewAllBrands", lst);
        }
        [HttpPost]
        public IActionResult saveBrand(Brand brand)
        {
            if (ModelState.IsValid)
            {
                //check name in db
                bool isBrandNameExist = _brandRepository.checkName(brand.BrandName);
                if (isBrandNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Brand is Exist");
                    return View("CreateBrand");
                }
                else
                {
                    _brandRepository.Create(brand);
                    return RedirectToAction("ViewAllBrands");
                }


            }
            else
            {
                return View("CreateBrand");
            }

        }
        public IActionResult CreateBrand()
        {

            return View("CreateBrand", new Brand());
        }
        public IActionResult EditBrand(int id)
        {

            return View("EditBrand", _brandRepository.GetBrandById(id));
        }
        [HttpPost]
        public IActionResult UpdateBrand(Brand brand)
        {

            _brandRepository.UpdateBrand(brand);
            return RedirectToAction("ViewAllBrands");
        }
        public IActionResult DeleteBrand(int id)
        {

            _brandRepository.DeleteBrand(id);
            return RedirectToAction("ViewAllBrands");

        }
    }
}
