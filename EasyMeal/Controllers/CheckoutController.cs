using System;
using System.Linq;
using EasyMeal.Domain.Interfaces;
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

        public ActionResult Index()
        {
            RetrieveWeekOrders();

            return View();
        }

        [HttpPost]
        public IActionResult Index(int weekOrderId)
        {
            RetrieveWeekOrders();
            var weekOrder = weekOrderRepository.GetWeekOrderById(weekOrderId);

            var result = weekOrderRepository.CompleteOrder(weekOrder);
            if (!result)
            {
                ModelState.AddModelError("Payment", $"Checkout failed. You have to order for atleast 4 workdays");
            }
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
        public void RetrieveWeekOrders()
        {
            var weekOrders = weekOrderRepository.GetWeekOrders();

            if(ViewBag.CompletedOrders == null)
            {
                ViewBag.CompletedOrders = weekOrders
                    .Where(x => x.Completed == true).ToList();
            }

            if (ViewBag.InCompletedOrders == null)
            {
                ViewBag.IncompleteOrders = weekOrders
                    .Where(x => x.Completed == false).ToList();
            }
        }
    }
}
