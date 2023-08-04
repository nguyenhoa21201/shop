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
    public class OrderDetailController : Controller
    {
        private IOrderDetailRepository _OrderDetailRepository;
        private ICustomerRepository _customerRepository;
        public OrderDetailController(IOrderDetailRepository OrderDetailRepository, ICustomerRepository customerRepository)
        {
            _OrderDetailRepository = OrderDetailRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAllOrderDetail()
        {
            List<OrderDetail> lst = _OrderDetailRepository.GetAll();
            return View("ViewAllOrderDetail", lst);
        }

        [HttpGet]
        public IActionResult CreateOrderDetail()
        {

            var q1 = from c in _customerRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.FullName,
                         Value = c.CustomerId.ToString(),
                     };

            ViewBag.CustomerId = q1.ToList();

            return View("CreateOrderDetail", new Product());
        }
        public IActionResult EditOrderDetail(int id)
        {
            var q1 = from c in _customerRepository.GetAll()
                     select new SelectListItem()
                     {
                         Text = c.FullName,
                         Value = c.CustomerId.ToString(),
                     };

            ViewBag.CustomerId = q1.ToList();
            return View("EditOrderDetail", _OrderDetailRepository.GetOrderDetailById(id));
        }
        [HttpPost]
        public IActionResult UpdateOrderDetail(OrderDetail OrderDetail)
        {



            _OrderDetailRepository.UpdateOrderDetail(OrderDetail);
            return RedirectToAction("ViewAllOrderDetail");
        }
        public IActionResult DeleteOrderDetail(int id)
        {

            _OrderDetailRepository.DeleteOrderDetail(id);
            return RedirectToAction("ViewAllOrderDetail");

        }

    }
}
