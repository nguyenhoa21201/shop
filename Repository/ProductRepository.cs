using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models;

namespace project.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        public bool Create(Product product);
        Product GetProductById(int id);
        bool AddProduct(Product product);
        bool UpdateProduct(Product product);
        public List<Product> GetAllProByCartId(int id);
        bool DeleteProduct(int id);
        public List<Product> searchProductByName(string productName);
        List<Product> GetAll();

        public bool checkName(string name);
    }

    public class ProductRepository : IProductRepository
    {
        private  GlassesShopContext _ctx;

        public ProductRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public List<Product> searchProductByName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                return _ctx.Products.ToList();
            }
            else
            {
                return _ctx.Products.Where(p => p.ProductName.Contains(productName)).ToList();

            }
        }
        public bool Create(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return true;
        }

        public bool checkName(string name)
        {
            Product c = _ctx.Products.Where(x => x.ProductName.Trim() == name.Trim()).FirstOrDefault();
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public List<Product> GetAllProducts()
        {
            return _ctx.Products.Include(p => p.CategoryId).ToList();
        }
        public List<Product> GetAll()
        {
             return _ctx.Products.ToList();

        }

        public Product GetProductById(int id)
        {
            Product c = _ctx.Products.FirstOrDefault(x=>x.ProductId==id);
            return c;
        }

        public bool AddProduct(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            _ctx.Products.Update(product);
            _ctx.SaveChanges();
            return true;
        }

       
        public bool DeleteProduct(int productid)
        {
            Product c = _ctx.Products.FirstOrDefault(x => x.ProductId == productid);
            _ctx.Products.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

        public List<Product> GetAllProByCartId(int id)
        {
           return _ctx.Products.Where(x=>x.CategoryId==id).ToList();
        }
    }

}
