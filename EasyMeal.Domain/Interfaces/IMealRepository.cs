using EasyMeal.Domain.ViewModels;
using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure
{
    public interface IMealRepository
    {
        IQueryable<Meal> GetMeals();
        IQueryable<MealResult> CurrentWeekMeals();
        IQueryable<MealResult> NextWeekMeals();
        Meal GetMealByID(int mealId);
        void InsertMeal(Meal meal);
        void UpdateMeal(int origin, Meal meal);
        void DeleteMeal(int mealId);
    }
}
