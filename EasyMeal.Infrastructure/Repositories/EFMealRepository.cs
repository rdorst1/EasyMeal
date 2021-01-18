using EasyMeal.Domain.Models;
using EasyMeal.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure
{
    public class EFMealRepository : IMealRepository
    {
        private MealManagementDbContext context;

        public EFMealRepository(MealManagementDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Meal> GetMeals() => context.Meals;
        public Meal GetMealByID(int mealId)
        {
            return context.Meals.Find(mealId);
        }

        public void InsertMeal(Meal meal)
        {
            context.Meals.Add(meal);
            context.SaveChanges();
        }

        public void UpdateMeal(int origin, Meal meal)
        {
            var toBeUpdatedMeal = context.Meals.FirstOrDefault(x => x.MealId == origin);

            if(toBeUpdatedMeal != null)
            {
                toBeUpdatedMeal.Name = meal.Name;
                toBeUpdatedMeal.Price = meal.Price;
                toBeUpdatedMeal.Description = meal.Description;
                toBeUpdatedMeal.Appetizer = meal.Appetizer;
                toBeUpdatedMeal.MainDish = meal.MainDish;
                toBeUpdatedMeal.Dessert = meal.Dessert;
                toBeUpdatedMeal.ChefId = meal.ChefId;
                toBeUpdatedMeal.Date = meal.Date;
                toBeUpdatedMeal.Saltless = meal.Saltless;
                toBeUpdatedMeal.Diabetes = meal.Diabetes;
                toBeUpdatedMeal.GlutenAllergy = meal.GlutenAllergy;
                toBeUpdatedMeal.ImageData = meal.ImageData;

                context.Meals.Update(toBeUpdatedMeal);
                context.SaveChanges();
            }
        }

        public void DeleteMeal(int mealId)
        {
            Meal meal = context.Meals.Find(mealId);
            context.Meals.Remove(meal);
            context.SaveChanges();
        }

        public IQueryable<MealResult> CurrentWeekMeals()
        {
            throw new NotImplementedException();
        }

        public IQueryable<MealResult> NextWeekMeals()
        {
            throw new NotImplementedException();
        }
    }
}
