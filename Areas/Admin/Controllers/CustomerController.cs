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

    public class CustomerController : Controller
    {
        private ICustomerRepository _CustomerRepository;
        private IUserRepository _UserRepository;
        public CustomerController(ICustomerRepository CustomerRepository, IUserRepository userRepository)
        {
            _CustomerRepository = CustomerRepository;
            _UserRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllCustomers()
        {
            List<Customer> lst = _CustomerRepository.GetAll();
            return View("ViewAllCustomers", lst);
        }
        [HttpPost]
        public IActionResult saveCustomer(Customer Customer)
        {
            if (ModelState.IsValid)
            {
                //check name in db
                bool isCustomerNameExist = _CustomerRepository.checkName(Customer.FullName);
                if (isCustomerNameExist)
                {
                    ModelState.AddModelError(string.Empty, "Customer is Exist");
                    return View("CreateCustomer");
                }
                else
                {
                    _CustomerRepository.Create(Customer);
                    return RedirectToAction("ViewAllCustomers");
                }


            }
            else
            {
                return View("CreateCustomer");
            }

        }
       
        public IActionResult EditCustomer(int id)
        {

            return View("EditCustomer", _CustomerRepository.GetCustomerById(id));
        }
        [HttpPost]
        public IActionResult UpdateCustomer(Customer Customer)
        {

            _CustomerRepository.UpdateCustomer(Customer);
            return RedirectToAction("ViewAllCustomers");
        }
        public IActionResult DeleteCustomer(int id)
        {

            _CustomerRepository.DeleteCustomer(id);
            return RedirectToAction("ViewAllCustomers");

        }
        [HttpGet]
        public IActionResult CreateCustomer()
        {

            var q1 = from c in _UserRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.Username,
                         Value = c.UserId.ToString(),
                     };

            ViewBag.UserId = q1.ToList();


            return View("CreateCustomer", new Customer());
        }
    }
}
