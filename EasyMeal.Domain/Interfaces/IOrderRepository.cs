using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrders();

        Order GetSpecificOrder(int mealId, string email);

        Order GetOrderOnThisDay(DateTime date, string email);

        Order GetOrderById(int id);

        IQueryable<Order> GetOrdersByUserId(int userId);

        IQueryable<Order> GetOrdersByEmail(string email);

        public void InsertOrder(Order order);

        public void UpdateOrder(Order order);
        public void DeleteOrder(int id);

    }
}
