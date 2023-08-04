using Microsoft.EntityFrameworkCore;
using project.Models;
using project.Models;

namespace project.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUser();
        public bool Create(User User);
        User GetUserById(int id);
        bool AddUser(User User);
        bool UpdateUser(User User);
        bool DeleteUser(int id);
        List<User> GetAll();
        public bool checkName(string name);
    }

    public class UserRepository : IUserRepository
    {
        private GlassesShopContext _ctx;

        public UserRepository(GlassesShopContext ctx)
        {
            _ctx = ctx;
        }
        public bool Create(User User)
        {
            _ctx.Users.Add(User);
            _ctx.SaveChanges();
            return true;
        }

        public bool checkName(string name)
        {
            User c = _ctx.Users.Where(x => x.Username.Trim() == name.Trim()).FirstOrDefault();
            if (c == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public List<User> GetAllUser()
        {
            return _ctx.Users.ToList();
        }
        public List<User> GetAll()
        {
            return _ctx.Users.ToList();

        }

        public User GetUserById(int id)
        {
            User c = _ctx.Users.FirstOrDefault(x => x.UserId == id);
            return c;
        }

        public bool AddUser(User User)
        {
            _ctx.Users.Add(User);
            _ctx.SaveChanges();
            return true;
        }

        public bool UpdateUser(User User)
        {
            _ctx.Users.Update(User);
            _ctx.SaveChanges();
            return true;
        }


        public bool DeleteUser(int Userid)
        {
            User c = _ctx.Users.FirstOrDefault(x => x.UserId == Userid);
            _ctx.Users.Remove(c);
            _ctx.SaveChanges();
            return true;
        }

    }

}
