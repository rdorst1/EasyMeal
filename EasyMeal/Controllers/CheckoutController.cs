using System;
using System.Linq;
using EasyMeal.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyMealOrder.Controllers
{
    public class CheckoutController : Controller
    {
        private static IWeekOrderRepository weekOrderRepository;
        private static IOrderRepository orderRepository;

        public CheckoutController(IWeekOrderRepository weekRepo, IOrderRepository orderRepo)
        {
            weekOrderRepository = weekRepo;
            orderRepository = orderRepo;
        }

        public IActionResult Index()
        {
            var weekOrders = weekOrderRepository.GetWeekOrders();

            ViewBag.CompletedOrders = weekOrders
                .Include(x => x.Orders)
                .Where(x => x.Completed == true).ToList();

            ViewBag.IncompleteOrders = weekOrders
                .Include(x => x.Orders)
                .Where(x => x.Completed == false).ToList();

            return View();
        }

        public static string CheckForDiscount(string email)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var orders = orderRepository.GetOrdersByEmail(email);

            var count = orders.Where(x => x.Date > firstDayOfMonth && x.Date < lastDayOfMonth && x.Price != 0).Count();

            if(count >= 15)
            {
                return "You have " + count + " orders this month, which means you'll receive a 10% discount at checkout!";
            }
            else
            {
                var left = 15 - count;
                return "You need to order " + left + " more meals this month to receive a 10% discount!";
            }
        }

        public static bool boolCheckForDiscount(string email)
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var orders = orderRepository.GetOrdersByEmail(email);

            var count = orders.Where(x => x.Date > firstDayOfMonth && x.Date < lastDayOfMonth && x.Price != 0).Count();

            if (count >= 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IActionResult CompleteOrder(int id)
        {
            var weekOrder = weekOrderRepository.GetWeekOrderById(id).FirstOrDefault();

            if (boolCheckForDiscount(User.Identity.Name) == true)
            {
                weekOrder.TotalPrice = decimal.Multiply(weekOrder.TotalPrice, (decimal)0.9);
            }

            weekOrderRepository.CompleteOrder(weekOrder);
            return RedirectToAction("Index");
        }
    }
}
