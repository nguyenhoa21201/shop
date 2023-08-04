using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models;

namespace project.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetAllCustomer();
        public bool Create(Customer Customer);
        Customer GetCustomerById(int id);
        Customer GetCustomerByEmail(string email);
        bool AddCustomer(Customer Customer);
        bool UpdateCustomer(Customer Customer);
        bool DeleteCustomer(int id);
        List<Customer> GetAll();
        public bool checkName(string name);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private GlassesShopContext _ctx;

        public CustomerRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Customer Customer)
        {
            _ctx.Customers.Add(Customer);
            _ctx.SaveChanges();
            return true;
        }

        public bool checkName(string name)
        {
            Customer c = _ctx.Customers.Where(x => x.FullName.Trim() == name.Trim()).FirstOrDefault();
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public List<Customer> GetAllCustomer()
        {
            return _ctx.Customers.ToList();
        }
        public List<Customer> GetAll()
        {
            return _ctx.Customers.ToList();

        }

        public Customer GetCustomerById(int id)
        {
            Customer c = _ctx.Customers.FirstOrDefault(x => x.CustomerId == id);
            return c;
        }

        public bool AddCustomer(Customer Customer)
        {
            _ctx.Customers.Add(Customer);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateCustomer(Customer Customer)
        {
            _ctx.Customers.Update(Customer);
            _ctx.SaveChanges();
            return true;
        }


        public bool DeleteCustomer(int Customerid)
        {
            Customer c = _ctx.Customers.FirstOrDefault(x => x.CustomerId == Customerid);
            _ctx.Customers.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

        public Customer GetCustomerByEmail(string email)
        {
            Customer c = _ctx.Customers.FirstOrDefault(x => x.Email == email);
            return c;
        }
    }

}
