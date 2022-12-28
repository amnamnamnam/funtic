using FUNTIK.Data;
using Microsoft.EntityFrameworkCore;

namespace FUNTIK.Models.Repositories
{
    public interface IUserRepository
    {
        public void Create(UserDa user);
        public void Delete(UserDa user);
        public UserDa? Find(Func<UserDa, bool> func);
        public UserDa? FindUserByEmail(string email);
        public List<UserDa> FindAll(Func<UserDa, bool> func);
        public void Update(UserDa user);
    }



    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext context;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(UserDa user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Delete(UserDa user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public UserDa? FindUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(i => i.Email == email);
        }
        
        public UserDa? FindUserFullInfoByEmail(string email)
        {
            return context.Users.FirstOrDefault(i => i.Email == email);
        }

        public UserDa? Find(Func<UserDa, bool> func)
        {
            return context.Users.Include(r => r.Recipes).FirstOrDefault(func);
        }


        public List<UserDa> FindAll(Func<UserDa, bool> func)
        {
            return context.Users.Where(func).ToList();
        }

        public void Update(UserDa user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
