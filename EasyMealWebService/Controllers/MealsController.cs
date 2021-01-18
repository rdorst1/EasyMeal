using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using EasyMeal.Domain.Models;
using EasyMeal.Domain.ViewModels;
using EasyMeal.Infrastructure;

namespace EasyMealWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private IMealRepository mealRepository;
        private readonly MealManagementDbContext _context;
        public List<Chef> chefs;
        public static DateTime StartOfCurrentWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        public static DateTime EndOfCurrentWeek = StartOfCurrentWeek.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);

        public MealsController(IMealRepository mealRepo)
        {
            mealRepository = mealRepo;
        }

        // Get: Meals for the current week
        [HttpGet("CurrentWeek")]
        public IQueryable<MealResult> GetMealsFromCurrentWeek()
        {
            return mealRepository.CurrentWeekMeals();
        }

        // Get: Meals for the next week
        [HttpGet("NextWeek")]
        public IQueryable<MealResult> GetMealsFromNextWeek()
        {
            return mealRepository.NextWeekMeals();
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public ActionResult<Meal> GetMeal(int id)
        {
            return mealRepository.GetMealByID(id);
        }

        private IActionResult NotSupportedException()
        {
            throw new NotImplementedException();
        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut]
        public IActionResult PutMeal() => NotSupportedException();

        // POST: api/Meals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public IActionResult PostMeal() => NotSupportedException();

        // DELETE: api/Meals/5
        [HttpDelete]
        public IActionResult DeleteMeal() => NotSupportedException();
    }
}
