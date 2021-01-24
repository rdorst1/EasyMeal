using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain.Interfaces
{
    public interface IWeekOrderRepository
    {
        IList<WeekOrder> GetWeekOrders();
        WeekOrder GetWeekOrderById(int id);
        IList<WeekOrder> GetWeekOrdersByCustomerId(int id);
        public void AddOrderToWeekOrder(int customerId, Order order);
        public void InsertWeekOrder(WeekOrder weekOrder);
        public void AddOrderToWeekOrder(WeekOrder weekOrder, Order order);
        public bool CompleteOrder(WeekOrder weekOrder);
    }
}
