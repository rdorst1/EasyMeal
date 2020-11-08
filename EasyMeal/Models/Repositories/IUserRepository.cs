using EasyMeal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMealOrder.Models.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }

        User GetUserById(int id);
        User GetUserByEmail(string email);
        void InsertUser(User user);
        void UpdateUser(User user);
    }
}
