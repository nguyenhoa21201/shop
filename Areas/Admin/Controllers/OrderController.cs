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

    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllOrder()
        {
            List<Order> lst = _orderRepository.GetAll();
            return View("ViewAllOrder", lst);
        }

        [HttpGet]
        public IActionResult CreateOrder()
        {

            var q1 = from c in _customerRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.FullName,
                         Value = c.CustomerId.ToString(),
                     };
            
            ViewBag.CustomerId = q1.ToList();
          
            return View("CreateOrder", new Product());
        }
        public IActionResult EditOrder(int id)
        {
            var q1 = from c in _customerRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.FullName,
                         Value = c.CustomerId.ToString(),
                     };

            ViewBag.CustomerId = q1.ToList();
            return View("EditOrder", _orderRepository.GetOrderById(id));
        }
        [HttpPost]
        public IActionResult UpdateOrder(Order Order)
        {

          

            _orderRepository.UpdateOrder(Order);
            return RedirectToAction("ViewAllOrder");
        }
        public IActionResult DeleteOrder(int id)
        {

            _orderRepository.DeleteOrder(id);
            return RedirectToAction("ViewAllOrder");

        }
      
    }
}
