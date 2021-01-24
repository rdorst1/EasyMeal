using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using EasyMeal.Domain.ViewModels;
using EasyMeal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMeal.WSRepository
{
    public class WSMealRepository : IMealRepository
    {
        private MealManagementDbContext context;
        private static DateTime startOfCurrentWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        private static DateTime endOfCurrentWeek = startOfCurrentWeek.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);

        public WSMealRepository(MealManagementDbContext ctx)
        {
            context = ctx;
        }

        public Meal GetMealByID(int mealId)
        {
            return context.Meals.Find(mealId);
        }

        public IQueryable<Meal> GetMeals() => context.Meals;

        public IQueryable<MealResult> CurrentWeekMeals()
        {
            return context.Meals
                 .Join(context.Chefs,
                     meal => meal.ChefId,
                     chef => chef.ChefId,
                     (meal, chef) => new MealResult
                     {
                         MealId = meal.MealId,
                         Name = meal.Name,
                         Price = meal.Price,
                         Description = meal.Description,
                         Appetizer = meal.Appetizer,
                         MainDish = meal.MainDish,
                         Dessert = meal.Dessert,
                         Date = meal.Date,
                         Saltless = meal.Saltless,
                         Diabetes = meal.Diabetes,
                         GlutenAllergy = meal.GlutenAllergy,
                         ChefId = chef.ChefId,
                         FirstName = chef.FirstName,
                         LastName = chef.LastName
                     })
                 .Where(x => x.Date > startOfCurrentWeek && x.Date < endOfCurrentWeek);
        }

        public IQueryable<MealResult> NextWeekMeals()
        {
            return context.Meals
                 .Join(context.Chefs,
                     meal => meal.ChefId,
                     chef => chef.ChefId,
                     (meal, chef) => new MealResult
                     {
                         MealId = meal.MealId,
                         Name = meal.Name,
                         Price = meal.Price,
                         Description = meal.Description,
                         Appetizer = meal.Appetizer,
                         MainDish = meal.MainDish,
                         Dessert = meal.Dessert,
                         Date = meal.Date,
                         Saltless = meal.Saltless,
                         Diabetes = meal.Diabetes,
                         GlutenAllergy = meal.GlutenAllergy,
                         ChefId = chef.ChefId,
                         FirstName = chef.FirstName,
                         LastName = chef.LastName
                     })
                 .Where(x => x.Date > startOfCurrentWeek.AddDays(7) && x.Date < endOfCurrentWeek.AddDays(7));
        }

        public void DeleteMeal(int mealId)
        {
            throw new NotImplementedException();
        }
        public void InsertMeal(Meal meal)
        {
            throw new NotImplementedException();
        }
        public void UpdateMeal(int origin, Meal meal)
        {
            throw new NotImplementedException();
        }
    }
}
