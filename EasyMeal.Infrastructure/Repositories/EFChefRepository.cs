using EasyMeal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure
{
    public class EFChefRepository : IChefRepository
    {
        private MealManagementDbContext context;

        public EFChefRepository(MealManagementDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Chef> GetChefs() => context.Chefs;
        public Chef GetChefByID(int chefId)
        {
            return context.Chefs.Find(chefId);
        }
        public void InsertChef(Chef chef)
        {
            context.Chefs.Add(chef);
            context.SaveChanges();
        }
        public void UpdateChef(int origin, Chef chef)
        {
            var toBeUpdatedChef = context.Chefs.FirstOrDefault(x => x.ChefId == origin);

            if (toBeUpdatedChef != null)
            {
                toBeUpdatedChef.FirstName = chef.FirstName;
                toBeUpdatedChef.LastName = chef.LastName;
                toBeUpdatedChef.TelephoneNumber = chef.TelephoneNumber;
                toBeUpdatedChef.Email = chef.Email;

                context.Chefs.Update(toBeUpdatedChef);
                context.SaveChanges();
            }
        }

        public void DeleteChef(int chefId)
        {
            Chef chef = context.Chefs.Find(chefId);
            context.Chefs.Remove(chef);
            context.SaveChanges();
        }

    }
}
