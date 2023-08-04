using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using project.Models;
using project.Repository;

namespace project.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        public CartController(IProductRepository productRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public IActionResult Index()
        {

            ViewBag.sessionId = HttpContext.Session.Id;
            CartModel cartModel = new CartModel();
            cartModel.CartId = HttpContext.Session.Id;
            if (HttpContext.Session.Get<List<Item>>("cart") != null)
            {
                List<Item>? items = HttpContext.Session.Get<List<Item>>("cart");
                cartModel.setAllItems(items);
            }
            return View(cartModel);

        }
        [HttpPost]
        public IActionResult Success()
        {
            List<Item>? items = HttpContext.Session.Get<List<Item>>("cart");
            CartModel cartModel = new CartModel();
            cartModel.setAllItems(items);
            var name = Request.Form["Name"].ToString();
            var email = Request.Form["Email"].ToString();
            var address = Request.Form["Address"].ToString();
            if (_customerRepository.GetCustomerByEmail(email) == null)
            {
                Customer customer = new Customer();
                customer.FullName = name;
                customer.Email = email;
                customer.Address = address;
                _customerRepository.AddCustomer(customer);
            }
            else
            {
                Customer customer = _customerRepository.GetCustomerByEmail(email);
                customer.FullName = name;
                customer.Email = email;
                customer.Address = address;
                _customerRepository.UpdateCustomer(customer);
            }
            // 1 => shoppingcart
            OrderSuccess orderSuccess = new OrderSuccess();
            orderSuccess.FullName = name;
            orderSuccess.Email = email;
            orderSuccess.Address = address;
            orderSuccess.Total = (int)cartModel.getTotal();
            orderSuccess.OrderDate = DateTime.Now;
            // 2 => insert order
            Order order = new Order();
            order.CustomerId = _customerRepository.GetCustomerByEmail(email).CustomerId;
            order.OrderDate = DateTime.Now;
            order.Total = (int)cartModel.getTotal();
            _orderRepository.AddOrder(order);
            foreach (var item in items)
            {
                // 3 => insert orderdetails
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.OrderId = order.OrderId;
                orderDetail.ProductId = item.Id;
                orderDetail.Quantity = item.Quantity;
                orderDetail.Total = item.lineTotal;
                orderDetail.Price = item.Price;
                _orderDetailRepository.AddOrderDetail(orderDetail);
            }
            HttpContext.Session.Remove("cart");
            return View("success",orderSuccess);
        }
        public IActionResult AddToCart(int id)
        {
            Product product = _productRepository.GetProductById(id);
            int quantity = 1;
            CartModel cartModel = null;
            if (HttpContext.Session.Get<List<Item>>("cart") != null)
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<Item>>("cart"));
            }
            else
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
            }
            Item item = new Item()
            {
                Id = product.ProductId,
                Name = product.ProductName,
                Price = (decimal)product.Price,
                ImagePath = product.ProductImagePath,
                Quantity = quantity,
                lineTotal = (decimal)(product.Price * quantity),
            };
            cartModel.addItem(item);
            //save to session
            HttpContext.Session.Set<List<Item>>("cart", cartModel.getAllItems());
            return RedirectToAction("Index");
        }
        public IActionResult UpdateQuantity()
        {
            var btn = Request.Form["btnUpdateQuantity"].ToString();
            var id = Request.Form["item.Id"].ToString();
            int productId = int.Parse(id);
            var qty = Request.Form["item.Quantity"].ToString();
            CartModel cartModel = new CartModel();
            if (HttpContext.Session.Get<List<Item>>("cart") != null)
            {
                cartModel = new CartModel();
                cartModel.CartId = HttpContext.Session.Id;
                cartModel.setAllItems(HttpContext.Session.Get<List<Item>>("cart"));
            }
            cartModel.UpdateQuantity(productId, 1, btn);
            HttpContext.Session.Set<List<Item>>("cart", cartModel.getAllItems());
            return RedirectToAction("Index");
        }
        public ActionResult Checkout()
        {
            List<Item>? items = HttpContext.Session.Get<List<Item>>("cart");
            CartModel cartModel = new CartModel();
            cartModel.setAllItems(items);
            // 1 => shoppingcart
            // 2 => insert order
            foreach (var item in items)
            {

                // 3 => insert orderdetails
            }
            return View(cartModel);
        }

    }
}
