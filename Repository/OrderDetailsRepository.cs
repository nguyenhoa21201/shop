using project.Models;

namespace project.Repository
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetAllOrderDetail();
        public bool Create(OrderDetail OrderDetail);
        OrderDetail GetOrderDetailById(int id);
        bool AddOrderDetail(OrderDetail OrderDetail);
        bool UpdateOrderDetail(OrderDetail OrderDetail);
        bool DeleteOrderDetail(int id);
        List<OrderDetail> GetAll();

    }
    public class OrderDetailRepository : IOrderDetailRepository
    {

        private GlassesShopContext _ctx;
        public OrderDetailRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(OrderDetail OrderDetail)
        {
            _ctx.OrderDetails.Add(OrderDetail);
            _ctx.SaveChanges();
            return true;
        }




        public List<OrderDetail> GetAllOrderDetail()
        {
            return _ctx.OrderDetails.ToList();
        }
        public List<OrderDetail> GetAll()
        {
            return _ctx.OrderDetails.ToList();

        }

        public OrderDetail GetOrderDetailById(int id)
        {
            OrderDetail c = _ctx.OrderDetails.FirstOrDefault(x => x.OrderDetailId == id);
            return c;
        }

        public bool AddOrderDetail(OrderDetail OrderDetail)
        {
            _ctx.OrderDetails.Add(OrderDetail);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateOrderDetail(OrderDetail OrderDetail)
        {
            _ctx.OrderDetails.Update(OrderDetail);
            _ctx.SaveChanges();
            return true;
        }


        public bool DeleteOrderDetail(int OrderDetailid)
        {
            OrderDetail c = _ctx.OrderDetails.FirstOrDefault(x => x.OrderDetailId == OrderDetailid);
            _ctx.OrderDetails.Remove(c);
            _ctx.SaveChanges();
            return true;
        }
    }
}
