namespace project.Models
{
    public class ProductDAO
    {
        private GlassesShopContext db = new GlassesShopContext();
        public List<Product> GetAllProducts()
        {
            return db.Products.ToList();
        }
        public void Add(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
        public void Update( Product product)
        {
            db.Entry(product).State= Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public void Delete(int id)
        {
            var product = db.Products.Find();
            db.Products.Remove(product);
            db.SaveChanges(); 
        }
        public Product findproductbyid(int id)
        {
            Product product = db.Products.Find(id);
            return product;
        }
    }
}
