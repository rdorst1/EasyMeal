using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain.Interfaces
{
    public interface IChefRepository
    {
        IQueryable<Chef> GetChefs();
        Chef GetChefByID(int chefId);
        void InsertChef(Chef chef);
        void UpdateChef(int origin, Chef chef);
        void DeleteChef(int chefId);
    }
}
