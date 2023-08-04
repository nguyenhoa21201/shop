using project.Models;

namespace project.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrder();
        public bool Create(Order Order);
        Order GetOrderById(int id);
        bool AddOrder(Order Order);
        bool UpdateOrder(Order Order);
        bool DeleteOrder(int id);
        List<Order> GetAll();
  
    }
    public class OrderRepository : IOrderRepository
    {
        
        private GlassesShopContext _ctx;
        public OrderRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Order Order)
        {
            _ctx.Orders.Add(Order);
            _ctx.SaveChanges();
            return true;
        }

       


        public List<Order> GetAllOrder()
        {
            return _ctx.Orders.ToList();
        }
        public List<Order> GetAll()
        {
            return _ctx.Orders.ToList();

        }

        public Order GetOrderById(int id)
        {
            Order c = _ctx.Orders.FirstOrDefault(x => x.OrderId == id);
            return c;
        }

        public bool AddOrder(Order Order)
        {
            _ctx.Orders.Add(Order);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateOrder(Order Order)
        {
            _ctx.Orders.Update(Order);
            _ctx.SaveChanges();
            return true;
        }


        public bool DeleteOrder(int Orderid)
        {
            Order c = _ctx.Orders.FirstOrDefault(x => x.OrderId == Orderid);
            _ctx.Orders.Remove(c);
            _ctx.SaveChanges();
            return true;
        }
    }
}
