using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using EasyMeal.Infrastructure;
using MealManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MealManagement.Controllers
{
    public class MealController : Controller
    {
        private IMealRepository mealRepository;
        private static IChefRepository chefRepository;
        private static int BUFFER_SIZE = 64 * 1024;
        private static string[] DaysOfTheWeek = new string[7]{"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};

        public MealController(IMealRepository mealRepo, IChefRepository chefRepo)
        {
            mealRepository = mealRepo;
            chefRepository = chefRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            var meals = mealRepository.GetMeals();

            DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            ViewBag.StartOfWeek = StartOfWeek.ToString("m");
            DateTime EndOfWeek = StartOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);
            ViewBag.EndOfWeek = EndOfWeek.ToString("m");

            DateTime NextWeekStart = StartOfWeek.AddDays(7);
            ViewBag.StartOfNextWeek = NextWeekStart.ToString("m");
            DateTime NextWeekEnd = EndOfWeek.AddDays(7);
            ViewBag.EndOfNextWeek = NextWeekEnd.ToString("m");

            var currentMeals = meals.Where(x => x.Date > StartOfWeek && x.Date < EndOfWeek);
            ViewBag.CurrentWeek = currentMeals;

            var nextMeals = meals.Where(x => x.Date > NextWeekStart && x.Date < NextWeekEnd);
            ViewBag.NextWeek = nextMeals;

            var nextTwoWeeks = meals.Where(x => x.Date > NextWeekStart.AddDays(7) && x.Date < NextWeekEnd.AddDays(7));
            ViewBag.TwoWeeks = nextTwoWeeks;

            ViewBag.DaysOfTheWeek = DaysOfTheWeek;

            return View();
        }

        public static IQueryable<Meal> MealsForToday(IQueryable<Meal> Meals, string day)
        {
            return Meals.Where(x => x.DayOfTheWeek == day);
        }

        public static string CheckMealsForToday(IQueryable<Meal> Meals, string day)
        {
            var errorString = "";

            var mealsOfToday = Meals.Where(x => x.DayOfTheWeek == day);
            if (mealsOfToday.Count() == 0)
            {
                errorString += "There are no meals for this day! \n";
            }
            else
            {
                if (mealsOfToday.Where(x => x.Diabetes == false).Count() == 0)
                {
                    errorString += "This day has no meal for people with diabetes! \n";
                }
                if (mealsOfToday.Where(x => x.GlutenAllergy == false).Count() == 0)
                {
                    errorString += "This day has no meal for people with gluten allergy! \n";
                }
                if (mealsOfToday.Where(x => x.Saltless == false).Count() == 0)
                {
                    errorString += "This day has no meal for people with Salt allergy! \n";
                }
            }

            return errorString;
        }

        public Meal GetMealByID(int id)
        {
            var meal = mealRepository.GetMealByID(id);
            return meal;
        }

        public static string GetChefName(int id)
        {
            var chef = chefRepository.GetChefByID(id);
            return chef.FirstName + " " + chef?.LastName;
        }

        public JsonResult ValidateDate(string Date)
        {
            DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            DateTime MealStartOfWeek = StartOfWeek.AddDays(14).AddHours(23).AddMinutes(59).AddSeconds(59);

            DateTime MealEndOfWeek = MealStartOfWeek.AddDays(7);

            DateTime parsedDate;

            if(!DateTime.TryParse(Date, out parsedDate))
            {
                return Json("Please enter a valid date dd/MM/yyyy HH-mm");
            } else if(DateTime.Now > parsedDate)
            {
                return Json("Please enter a date in the future");
            }else if (parsedDate > MealEndOfWeek)
            {
                return Json("Please enter a date before " + MealEndOfWeek);
            }else if(parsedDate < MealStartOfWeek)
            {
                return Json("Please enter a date after " + MealStartOfWeek);
            } else
            {
                return Json(true);
            }
        }

        public static byte[] Compress(byte[] inputData)
        {
            if (inputData == null)
                throw new ArgumentNullException("inputData must be non-null");

            using (var compressIntoMs = new MemoryStream())
            {
                using (var gzs = new BufferedStream(new GZipStream(compressIntoMs,
                 CompressionMode.Compress), inputData.Length))
                {
                    gzs.Write(inputData, 0, inputData.Length);
                }
                var data = compressIntoMs.ToArray();
                return data;
            }
        }

        public static byte[] Decompress(byte[] inputData)
        {
            if (inputData == null)
                throw new ArgumentNullException("inputData must be non-null");

            using (var compressedMs = new MemoryStream(inputData))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (var gzs = new BufferedStream(new GZipStream(compressedMs,
                     CompressionMode.Decompress), inputData.Length))
                    {
                        gzs.CopyTo(decompressedMs);
                    }
                    return decompressedMs.ToArray();
                }
            }
        }

        [Authorize]
        public ActionResult Create()
        {
            var meals = mealRepository.GetMeals();

            IEnumerable<SelectListItem> appetizers = meals
                .Select(x => new SelectListItem
                {
                    Value = x.MealId.ToString(),
                    Text = x.Appetizer
                });
            ViewBag.Appetizers = appetizers.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            IEnumerable<SelectListItem> mainDishes = meals
                .Select(x => new SelectListItem
                {
                    Value = x.MealId.ToString(),
                    Text = x.MainDish
                });
            ViewBag.MainDishes = mainDishes.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            IEnumerable<SelectListItem> desserts = meals
                .Select(x => new SelectListItem
                {
                    Value = x.MealId.ToString(),
                    Text = x.Dessert
                });
            ViewBag.Desserts = desserts.GroupBy(x => x.Text).Select(x => x.First()).ToList();

            IEnumerable<SelectListItem> chefs = chefRepository.GetChefs()
                .Select(x => new SelectListItem
                {
                    Value = x.ChefId.ToString(),
                    Text = x.FirstName + " " + x.LastName
                });
            ViewBag.Chefs = chefs;

            DateTime StartOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
            DateTime EndOfWeek = StartOfWeek.AddDays(7).AddHours(23).AddMinutes(59).AddSeconds(59);
            ViewBag.EndOfWeek = EndOfWeek;

            DateTime MealStartOfWeek = StartOfWeek.AddDays(14);
            ViewBag.MealStartOfWeek = MealStartOfWeek.ToString("dd/MM");
            DateTime MealEndOfWeek = MealStartOfWeek.AddDays(7);
            ViewBag.MealEndOfWeek = MealEndOfWeek.ToString("dd/MM");

            return View(new CreateMealViewModel() { Date = DateTime.Today });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMealViewModel model, IFormFile ImageData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ImageData.CopyToAsync(memoryStream);

                        if(memoryStream.Length < 2097152)
                        {
                            var data = memoryStream.ToArray();

                            Meal meal = new Meal
                            {
                                Name = model.Name,
                                Price = model.Price,
                                Description = model.Description,
                                Appetizer = model.Appetizer,
                                MainDish = model.MainDish,
                                Dessert = model.Dessert,
                                ChefId = model.ChefId,
                                Date = model.Date,
                                DayOfTheWeek = model.Date.DayOfWeek.ToString(),
                                Saltless = model.Saltless,
                                Diabetes = model.Diabetes,
                                GlutenAllergy = model.GlutenAllergy,
                                ImageData = Compress(data)
                            };
                            mealRepository.InsertMeal(meal);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save meal.");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Update(int id)
        {
            Meal meal = mealRepository.GetMealByID(id);
            return View(meal);
        }

        [HttpPost]
        public ActionResult Update(Meal model)
        {
            var id = RouteData.Values["id"].ToString();
            var ImageData = mealRepository.GetMealByID(Int32.Parse(id)).ImageData;

            try
            {
                if (ModelState.IsValid)
                {
                    Meal meal = new Meal
                    {
                        Name = model.Name,
                        Price = model.Price,
                        Description = model.Description,
                        Appetizer = model.Appetizer,
                        MainDish = model.MainDish,
                        Dessert = model.Dessert,
                        ChefId = model.ChefId,
                        Date = model.Date,
                        ImageData = ImageData,
                        Saltless = model.Saltless,
                        Diabetes = model.Diabetes,
                        GlutenAllergy = model.GlutenAllergy
                    };

                    if (model.Appetizer == null && model.Dessert == null)
                    {
                        ModelState.AddModelError("", "There has to be either an appetizer or a dessert");
                    }
                    else
                    {
                        mealRepository.UpdateMeal(Int32.Parse(id), meal);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "The file is too large.");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to update meal.");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes, try again later.";
            }
            Meal meal = mealRepository.GetMealByID(id);
            return View(meal);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Meal meal = mealRepository.GetMealByID(id);
                mealRepository.DeleteMeal(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                    new Microsoft.AspNetCore.Routing.RouteValueDictionary
                    {
                        { "id", id },
                        { "saveChangesError", true }
                    });
            }
            return RedirectToAction("Index");
        }
    }
}