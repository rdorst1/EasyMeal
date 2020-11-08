using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealManagement.Models.Repositories
{
    public class EFChefRepository : IChefRepository
    {
        private ApplicationDbContext context;

        public EFChefRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Chef> GetChefs() => context.Chef;
        public Chef GetChefByID(int chefId)
        {
            return context.Chef.Find(chefId);
        }
        public void InsertChef(Chef chef)
        {
            context.Chef.Add(chef);
            context.SaveChanges();
        }
        public void UpdateChef(int origin, Chef chef)
        {
            var toBeUpdatedChef = context.Chef.FirstOrDefault(x => x.ChefId == origin);

            if (toBeUpdatedChef != null)
            {
                toBeUpdatedChef.FirstName = chef.FirstName;
                toBeUpdatedChef.LastName = chef.LastName;
                toBeUpdatedChef.TelephoneNumber = chef.TelephoneNumber;
                toBeUpdatedChef.Email = chef.Email;

                context.Chef.Update(toBeUpdatedChef);
                context.SaveChanges();
            }
        }

        public void DeleteChef(int chefId)
        {
            Chef chef = context.Chef.Find(chefId);
            context.Chef.Remove(chef);
            context.SaveChanges();
        }

    }
}
