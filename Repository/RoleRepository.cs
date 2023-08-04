using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models;

namespace project.Repository
{
    public interface IRoleRepository
    {
        List<Role> GetAllRole();
        public bool Create(Role Role);
        Role GetRoleById(int id);
        bool AddRole(Role Role);
        bool UpdateRole(Role Role);
        bool DeleteRole(int id);
        List<Role> GetAll();
        public bool checkName(string name);
    }

    public class RoleRepository : IRoleRepository
    {
        private GlassesShopContext _ctx;

        public RoleRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(Role Role)
        {
            _ctx.Roles.Add(Role);
            _ctx.SaveChanges();
            return true;
        }

        public bool checkName(string name)
        {
            Role c = _ctx.Roles.Where(x => x.RoleName.Trim() == name.Trim()).FirstOrDefault();
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public List<Role> GetAllRole()
        {
            return _ctx.Roles.ToList();
        }
        public List<Role> GetAll()
        {
            return _ctx.Roles.ToList();

        }

        public Role GetRoleById(int id)
        {
            Role c = _ctx.Roles.FirstOrDefault(x => x.RoleId == id);
            return c;
        }

        public bool AddRole(Role Role)
        {
            _ctx.Roles.Add(Role);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateRole(Role Role)
        {
            _ctx.Roles.Update(Role);
            _ctx.SaveChanges();
            return true;
        }


        public bool DeleteRole(int Roleid)
        {
            Role c = _ctx.Roles.FirstOrDefault(x => x.RoleId == Roleid);
            _ctx.Roles.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

    }

}
