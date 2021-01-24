using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using EasyMeal.Infrastructure;
using EasyMealOrder.Models.ViewModels;
using EasyMealOrder.Models.ViewModels.OrderViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EasyMealOrder.Controllers
{
    public class OrderController : Controller
    {
        private static string[] DaysOfTheWeek = new string[5] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
        private static IOrderRepository orderRepository;
        private IWeekOrderRepository weekOrderRepository;
        private IUserRepository userRepository;
        private static IMealRepository mealRepository;

        public OrderController(IOrderRepository orderRepo, IWeekOrderRepository weekRepo, IUserRepository userRepo, IMealRepository mealRepo)
        {
            orderRepository = orderRepo;
            weekOrderRepository = weekRepo;
            userRepository = userRepo;
            mealRepository = mealRepo;
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

        public static string GetMealName(int mealId)
        {
            return mealRepository.GetMealByID(mealId).Name;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.CurrentWeek = mealRepository.CurrentWeekMeals();

            ViewBag.NextWeek = mealRepository.NextWeekMeals();

            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday).AddDays(-7);

            var user = userRepository.GetUserByEmail(User.Identity.Name);

            var orders = orderRepository.GetOrdersByUserId(user.UserId);

            var ordersForThisWeek = orders.Where(x => x.Date > startOfWeek);

            WeekOrder wo = weekOrderRepository
                .GetWeekOrdersByCustomerId(user.UserId)
                .Where(x => x.StartDate == startOfWeek.Date)
                .FirstOrDefault();

            ViewBag.TotalPrice = 0;

            if(wo != null)
            {
                ViewBag.TotalPrice = wo.TotalPrice;
            }

            DaysLeftToOrder(ordersForThisWeek);

            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            var mealId = int.Parse(RouteData.Values["id"].ToString());

            var meal = mealRepository.GetMealByID(mealId);

            ViewBag.Meal = meal;

            ViewBag.Day = meal.Date.DayOfWeek;

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