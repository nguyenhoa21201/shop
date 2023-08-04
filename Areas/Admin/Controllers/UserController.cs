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

    public class UserController : Controller

    {
        private IRoleRepository _roleRepository;
        private IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllUser()
        {
            //1. get data
            List<User> lst = _userRepository.GetAll();
            //2 passign data to view
            return View("ViewAllUser", lst);
        }
        [HttpPost]
        public IActionResult saveUser(User user)
        {
            if (ModelState.IsValid)
            {
                //check name in db
                bool isUserNameExist = _userRepository.checkName(user.Username);
                if (isUserNameExist)
                {
                    ModelState.AddModelError(string.Empty, "User is Exist");
                    return View("CreateUser");
                }
                else
                {
                    _userRepository.Create(user);
                    return RedirectToAction("ViewAllUser");
                }


            }
            else
            {
                return View("CreateUser");
            }

        }
      
        public IActionResult EditUser(int id)
        {
            var q1 = from c in _roleRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.RoleName,
                         Value = c.RoleId.ToString(),
                     };

            ViewBag.RoleId = q1.ToList();

            return View("EditUser", _userRepository.GetUserById(id));
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {

            _userRepository.UpdateUser(user);
            return RedirectToAction("ViewAllUser");
        }
        public IActionResult DeleteUser(int id)
        {

            _userRepository.DeleteUser(id);
            return RedirectToAction("ViewAllUser");

        }
        [HttpGet]
        public IActionResult CreateUser()
        {

            var q1 = from c in _roleRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.RoleName,
                         Value = c.RoleId.ToString(),
                     };
           
            ViewBag.RoleId = q1.ToList();
           

            return View("CreateUser", new User());
        }
    }
}
