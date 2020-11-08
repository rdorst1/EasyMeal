using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealManagement.Models.Repositories
{
    public interface IMealRepository
    {
        IQueryable<Meal> GetMeals();

        Meal GetMealByID(int mealId);
        void InsertMeal(Meal meal);
        void UpdateMeal(int origin, Meal meal);
        void DeleteMeal(int mealId);
    }
}
