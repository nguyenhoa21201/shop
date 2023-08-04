using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project.Models;
using project.Repository;
using System.Data;

namespace project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {

        private IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllRole()
        {
            //1. get data
            List<Role> lst = _roleRepository.GetAllRole();
            //2 passign data to view
            return View("ViewAllRole", lst);
        }
        [HttpPost]
        public IActionResult saveRole(Role role)
        {
            if (ModelState.IsValid)
            {
                //check name in db
                bool isRoleNameExist = _roleRepository.checkName(role.RoleName);
                if (isRoleNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Role is Exist");
                    return View("CreateRole");
                }
                else
                {
                    _roleRepository.Create(role);
                    return RedirectToAction("ViewAllRole");
                }


            }
            else
            {
                return View("CreateRole");
            }

        }
        public IActionResult CreateRole()
        {

            return View("CreateRole", new Role());
        }
        public IActionResult EditRole(int id)
        {

            return View("EditRole", _roleRepository.GetRoleById(id));
        }
        [HttpPost]
        public IActionResult UpdateRole(Role role)
        {

            _roleRepository.UpdateRole(role);
            return RedirectToAction("ViewAllRole");
        }
        public IActionResult DeleteRole(int id)
        {

            _roleRepository.DeleteRole(id);
            return RedirectToAction("ViewAllRole");

        }
    }
}
