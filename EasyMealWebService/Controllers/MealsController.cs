using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyMealWebService.Models;
using EasyMeal.Domain;
using EasyMeal.Infrastructure;

namespace EasyMealWebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly MealManagementDbContext _context;
        public List<Chef> chefs;
        public static DateTime StartOfCurrentWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
        public static DateTime EndOfCurrentWeek = StartOfCurrentWeek.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);

        public MealsController(MealManagementDbContext context)
        {
            _context = context;
        }

        // Get: Meals for the current week
        [HttpGet("CurrentWeek")]
        public IQueryable<MealResult> GetMealsFromCurrentWeek()
        {
            var data = from meal in _context.Meals
                       join chef in _context.Chefs on meal.ChefId equals chef.ChefId
                       where meal.Date > StartOfCurrentWeek && meal.Date < EndOfCurrentWeek
                       select new MealResult
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
                       };

            return data;
        }

        // Get: Meals for the next week
        [HttpGet("NextWeek")]
        public IQueryable<MealResult> GetMealsFromNextWeek()
        { 
            var data = from meal in _context.Meals
                       join chef in _context.Chefs on meal.ChefId equals chef.ChefId
                       where meal.Date > StartOfCurrentWeek.AddDays(7) && meal.Date < EndOfCurrentWeek.AddDays(7)
                       select new MealResult
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
                       };

            return data;
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Meal>> GetMeal(int id)
        {
            var meal = await _context.Meals.FindAsync(id);

            if (meal == null)
            {
                return NotFound();
            }

            return meal;
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
