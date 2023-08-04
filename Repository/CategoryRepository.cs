using project.Models;

namespace project.Repository
{
    public interface ICategoryRepository
    {
        public bool Create(Category category);
        public bool Update(Category category);
        public bool Delete(int categoryId);
        public Category findById(int id);
        public List<Category> GetAll();
        public bool checkName(string name);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private GlassesShopContext _ctx;

        public CategoryRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }

        public bool checkName(string name)
        {
            Category c = _ctx.Categories.Where(x => x.CategoryName.Trim() == name.Trim()).FirstOrDefault();
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool Create(Category category)
        {
            _ctx.Categories.Add(category);
            _ctx.SaveChanges();
            return true;
        }

        public bool Delete(int categoryId)
        {
            Category c = _ctx.Categories.FirstOrDefault(x => x.CategoryId == categoryId);
            _ctx.Categories.Remove(c);
            _ctx.SaveChanges();
            return true;
        }
 
        public Category findById(int id)
        {
            Category c = _ctx.Categories.FirstOrDefault(x => x.CategoryId == id);
            return c;
        }

        public List<Category> GetAll()
        {
            return _ctx.Categories.ToList();
        }

        public bool Update(Category category)
        {
            Category c = _ctx.Categories.FirstOrDefault(x => x.CategoryId== category.CategoryId);
            if (c != null)
            {
                _ctx.Entry(c).CurrentValues.SetValues(category);
                _ctx.SaveChanges();


            }
            return true;
        }
    }
}
