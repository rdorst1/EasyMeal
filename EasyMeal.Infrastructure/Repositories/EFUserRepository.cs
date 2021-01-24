using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure.Repositories
{
    public class EFUserRepository: IUserRepository
    {
        private EasyMealDbContext context;
        public EFUserRepository(EasyMealDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<User> Users => context.Users;

        public User GetUserById(int id)
        {
            return context.Users.Where(x => x.UserId == id).FirstOrDefault();
        }

        public User GetUserByEmail(string email)
        {
            return context.Users.Where(x => x.Email == email).SingleOrDefault();
        }
        public void InsertUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }
    }
}
