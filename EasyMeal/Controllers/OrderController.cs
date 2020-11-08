using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyMealOrder.Models;
using EasyMealOrder.Models.Repositories;
using EasyMealOrder.Models.ViewModels;
using EasyMealOrder.Models.ViewModels.OrderViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EasyMealOrder.Controllers
{
    public class OrderController : Controller
    {
        private static string[] DaysOfTheWeek = new string[5] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        private static IOrderRepository orderRepository;
        private IWeekOrderRepository weekOrderRepository;
        private IUserRepository userRepository;

        public OrderController(IOrderRepository orderRepo, IWeekOrderRepository weekRepo, IUserRepository userRepo)
        {
            orderRepository = orderRepo;
            weekOrderRepository = weekRepo;
            userRepository = userRepo;
        }

        public static bool Ordered(int mealId, string email)
        {
            if(orderRepository.GetSpecificOrder(mealId, email) != null)
            {
                return true;
            }
            return false;
        }

        public static bool OrderOnThisDay(DateTime date, string email)
        {
            if(orderRepository.GetOrderOnThisDay(date, email) != null)
            {
                return true;
            }
            return false;
        }

        public static async Task<string> GetMealName(int mealId)
        {
            using (var client = new HttpClient())
            {
#if Debug
                client.BaseAddress = new Uri("https://localhost:5005/api/Meals/");
#endif
                client.BaseAddress = new Uri("https://easymealapird.azurewebsites.net/api/Meals/");

                var response = await client.GetAsync(mealId.ToString());
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();

                MealViewModel meal = JsonConvert.DeserializeObject<MealViewModel>(data);

                return meal.Name;
            }
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
#if Debug
                client.BaseAddress = new Uri("https://localhost:5005/api/Meals/");
#endif
                client.BaseAddress = new Uri("https://easymealapird.azurewebsites.net/api/Meals/");

                var response = await client.GetAsync("CurrentWeek");
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();

                List<MealViewModel> currentMeals = JsonConvert.DeserializeObject<List<MealViewModel>>(data);

                ViewBag.CurrentWeek = currentMeals;

                var secondResponse = await client.GetAsync("NextWeek");
                secondResponse.EnsureSuccessStatusCode();

                var nextWeekData = await secondResponse.Content.ReadAsStringAsync();

                List<MealViewModel> nextMeals = JsonConvert.DeserializeObject<List<MealViewModel>>(nextWeekData);

                ViewBag.NextWeek = nextMeals;
            }

            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(-7);

            var user = userRepository.GetUserByEmail(User.Identity.Name);

            var orders = orderRepository.GetOrdersByUserId(user.UserId);

            var ordersForThisWeek = orders.Where(x => x.Date > startOfWeek);

            ViewBag.TotalPrice = weekOrderRepository.CalculateTotalPrice(user.UserId);

            DaysLeftToOrder(ordersForThisWeek);

            return View();
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            using (var client = new HttpClient())
            {
#if Debug
                client.BaseAddress = new Uri("https://localhost:5005/api/Meals/");
#endif
                client.BaseAddress = new Uri("https://easymealapird.azurewebsites.net/api/Meals/");

                var id = RouteData.Values["id"].ToString();

                var response = await client.GetAsync(id);
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();

                MealViewModel meal = JsonConvert.DeserializeObject<MealViewModel>(data);

                ViewBag.Meal = meal;

                ViewBag.Day = meal.Date.DayOfWeek;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateModel model)
        {
            var user = userRepository.GetUserByEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    MealId = model.MealId,
                    Price = model.Price,
                    Appetizer = model.Appetizer,
                    Dessert = model.Dessert,
                    OrderSize = model.OrderSize,
                    Customer = user,
                    Date = model.Date,
                    Day = model.Day
                };

                switch (model.OrderSize)
                {
                    case Size.Small:
                        order.Price = decimal.Multiply(order.Price, (decimal)0.8);
                        break;
                    case Size.Large: 
                        order.Price = decimal.Multiply(order.Price, (decimal)1.2);
                        break;
                }

                if (user.Birthday.Date == model.Date.Date)
                {
                    order.Price = 0;
                }

                orderRepository.InsertOrder(order);

                weekOrderRepository.AddOrderToWeekOrder(user.UserId, order);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Unable to save Order");
                return View();
            }
        }

        private void DaysLeftToOrder(IQueryable<Order> orders)
        {
            var daysList = new List<string>(DaysOfTheWeek);

            foreach (var order in orders)
            {
                switch (order.Date.DayOfWeek.ToString())
                {
                    case "Monday":
                        daysList.Remove("Monday");
                        break;
                    case "Tuesday":
                        daysList.Remove("Tuesday");
                        break;
                    case "Wednesday":
                        daysList.Remove("Wednesday");
                        break;
                    case "Thursday":
                        daysList.Remove("Thursday");
                        break;
                    case "Friday":
                        daysList.Remove("Friday");
                        break;
                    default:
                        break;
                }
            }

            if(daysList.Count() > 1)
            {
                var daysLeft = daysList.Count() - 1;

                var result = "You still have to order on " + daysLeft + " days.\n Available days: ";
                foreach (var item in daysList)
                {
                    result += item + ", ";
                }

                ViewBag.DaysLeftToOrder = result.TrimEnd(',', ' ') + ".";
            }
        }

        public ActionResult Update(int mealId, string email)
        {
            Order order = orderRepository.GetSpecificOrder(mealId, email);
            return View(order);
        }

        [HttpPost]
        public ActionResult Update(Order model)
        {
            var id = RouteData.Values["id"].ToString();
            var email = User.Identity.Name;
            Order order = orderRepository.GetSpecificOrder(int.Parse(id), email);

            if (ModelState.IsValid)
            {
                order.OrderSize = model.OrderSize;
                order.Appetizer = model.Appetizer;
                order.Dessert = model.Dessert;

                orderRepository.UpdateOrder(order);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete()
        {
            var id = RouteData.Values["id"].ToString();
            var email = User.Identity.Name;
            var order = orderRepository.GetSpecificOrder(int.Parse(id), email);

            orderRepository.DeleteOrder(order.OrderId);

            return RedirectToAction("Index");
        }
    }
}