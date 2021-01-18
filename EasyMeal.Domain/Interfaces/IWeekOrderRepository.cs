using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Infrastructure
{
    public interface IWeekOrderRepository
    {
        IQueryable<WeekOrder> GetWeekOrders();
        IQueryable<WeekOrder> GetWeekOrderById(int id);
        IQueryable<WeekOrder> GetWeekOrdersByCustomerId(int id);
        public void AddOrderToWeekOrder(int customerId, Order order);
        public void InsertWeekOrder(WeekOrder weekOrder);
        public void AddOrderToWeekOrder(WeekOrder weekOrder, Order order);
        public decimal CalculateTotalPrice(int customerId);
        public void CompleteOrder(WeekOrder weekOrder);
    }
}
