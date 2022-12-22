using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;

namespace FUNTIK.Models.Repositories
{
    public interface IUserRepository
    {
        public void Create(User user);
        public void Delete(User user);
        public User? Find(Func<User, bool> func);
        public List<User> FindAll(Func<User, bool> func);
        public void Update(User user);
    }



    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public User? Find(Func<User, bool> func)
        {
            return context.Users.Include(r => r.Recipes).FirstOrDefault(func);
        }

        public List<User> FindAll(Func<User, bool> func)
        {
            return context.Users.Where(func).ToList();
        }

        public void Update(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
