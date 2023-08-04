using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models;

namespace project.Repository
{
    public interface IBrandRepository
    {
        List<Brand> GetAllBrand();
        public bool Create(Brand Brand);
        Brand GetBrandById(int id);
        bool AddBrand(Brand Brand);
        bool UpdateBrand(Brand Brand);
        bool DeleteBrand(int id);
        List<Brand> GetAll();
        public bool checkName(string name);
    }

    public class BrandRepository : IBrandRepository
    {
        private GlassesShopContext _ctx;

        public BrandRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Brand brand)
        {
            _ctx.Brands.Add(brand);
            _ctx.SaveChanges();
            return true;
        }

        public bool checkName(string name)
        {
            Brand c = _ctx.Brands.Where(x => x.BrandName.Trim() == name.Trim()).FirstOrDefault();
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public List<Brand> GetAllBrand()
        {
            return _ctx.Brands.ToList();
        }
        public List<Brand> GetAll()
        {
            return _ctx.Brands.ToList();

        }

        public Brand GetBrandById(int id)
        {
            Brand c = _ctx.Brands.FirstOrDefault(x => x.BrandId == id);
            return c;
        }

        public bool AddBrand(Brand Brand)
        {
            _ctx.Brands.Add(Brand);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateBrand(Brand Brand)
        {
            _ctx.Brands.Update(Brand);
            _ctx.SaveChanges();
            return true;
        }


        public bool DeleteBrand(int Brandid)
        {
            Brand c = _ctx.Brands.FirstOrDefault(x => x.BrandId == Brandid);
            _ctx.Brands.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

    }

}
