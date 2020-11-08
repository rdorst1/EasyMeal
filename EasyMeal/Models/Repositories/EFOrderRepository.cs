using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EasyMealOrder.Models.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private EasyMealDbContext context;

        public EFOrderRepository(EasyMealDbContext ctx)
        {
            context = ctx;
        }


        public IQueryable<Order> GetOrders() => context.Orders; 
        public Order GetSpecificOrder(int mealId, string email)
        {
            return context.Orders.Where(x => x.MealId == mealId && x.Customer.Email == email).FirstOrDefault();
        }

        public Order GetOrderOnThisDay(DateTime date, string email)
        {
            return context.Orders.Where(x => x.Date.Date == date.Date && x.Customer.Email == email).FirstOrDefault();
        }

        public Order GetOrderById(int id) => context.Orders.Where(x => x.OrderId == id).FirstOrDefault();

        public IQueryable<Order> GetOrdersByUserId(int userId)
        {
            return context.Orders.Where(x => x.Customer.UserId == userId);
        }

        public IQueryable<Order> GetOrdersByEmail(string email)
        {
            return context.Orders.Where(x => x.Customer.Email == email);
        }

        public void InsertOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            context.Orders.Update(order);
            context.SaveChanges();
        }

        public void DeleteOrder(int id)
        {
            context.Orders.Remove(context.Orders.Where(x => x.OrderId == id).FirstOrDefault());
            context.SaveChanges();
        }
    }
}
