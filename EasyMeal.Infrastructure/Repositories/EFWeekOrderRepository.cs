using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure.Repositories
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
            }
        }

        public IList<WeekOrder> GetWeekOrders() => context.WeekOrders.Include(x => x.Orders).ToList();

        public WeekOrder GetWeekOrderById(int id) => context.WeekOrders.Include(x => x.Orders).Where(x => x.WeekOrderId == id).FirstOrDefault();

        public IList<WeekOrder> GetWeekOrdersByCustomerId(int id)
        {
            var weekOrders = context.WeekOrders.Include(x => x.Orders).Where(x => x.Customer.UserId == id);

            return weekOrders.ToList();
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

        public bool CompleteOrder(WeekOrder weekOrder)
        {
            var toBeCompleted = context.WeekOrders.Where(x => x.WeekOrderId == weekOrder.WeekOrderId).FirstOrDefault();
            if (toBeCompleted != null && toBeCompleted.CanCompleteOrder)
            {
                context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
