using EasyMeal.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure
{
    public class EFWeekOrderRepository : IWeekOrderRepository
    {
        private EasyMealDbContext context;
        private IUserRepository userRepo;

        public EFWeekOrderRepository(EasyMealDbContext ctx, IUserRepository userRepository)
        {
            context = ctx;
            userRepo = userRepository;
        }

        public void AddOrderToWeekOrder(int customerId, Order order)
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

            var weekOrder = context.WeekOrders.Where(x => x.Customer.UserId == customerId && x.StartDate >= startOfWeek && x.EndDate <= endOfWeek).FirstOrDefault();

            if(weekOrder == null)
            {
                WeekOrder wo = new WeekOrder
                {
                    Customer = userRepo.GetUserById(customerId),
                    StartDate = startOfWeek,
                    EndDate = endOfWeek
                };
                wo.Orders = new List<Order>
                {
                    order
                };
                InsertWeekOrder(wo);
            }
            else
            {
                AddOrderToWeekOrder(weekOrder, order);
                CalculateTotalPrice(customerId);
            }
        }

        public IQueryable<WeekOrder> GetWeekOrders() => context.WeekOrders.Include(x => x.Orders);

        public IQueryable<WeekOrder> GetWeekOrderById(int id) => context.WeekOrders.Include(x => x.Orders).Where(x => x.WeekOrderId == id);

        public IQueryable<WeekOrder> GetWeekOrdersByCustomerId(int id)
        {
            var weekOrders = context.WeekOrders.Where(x => x.Customer.UserId == id);

            return weekOrders;
        }

        public void InsertWeekOrder(WeekOrder weekOrder)
        {
            context.WeekOrders.Add(weekOrder);
            context.SaveChanges();
        }

        public void AddOrderToWeekOrder(WeekOrder weekOrder, Order order)
        {
            weekOrder.Orders = new List<Order>
                {
                    order
                };
            context.SaveChanges();
        }

        public decimal CalculateTotalPrice(int customerId)
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59);

            WeekOrder weekOrder = context.WeekOrders
                .Include(x => x.Orders)
                .Where(x => x.Customer.UserId == customerId && x.StartDate >= startOfWeek && x.EndDate <= endOfWeek && x.Completed == false).FirstOrDefault();

            decimal totalPrice = 0;

            if (weekOrder != null && weekOrder.Orders != null)
            {
                foreach (Order order in weekOrder.Orders)
                {
                    totalPrice += order.Price;
                }
                weekOrder.TotalPrice = totalPrice;
                context.SaveChanges();
            }

            return totalPrice;
        }

        public void CompleteOrder(WeekOrder weekOrder)
        {
            var toBeCompleted = context.WeekOrders.Where(x => x.WeekOrderId == weekOrder.WeekOrderId).FirstOrDefault();
            if (toBeCompleted != null)
            {
                toBeCompleted.Completed = true;
                context.SaveChanges();
            }
        }
    }
}
