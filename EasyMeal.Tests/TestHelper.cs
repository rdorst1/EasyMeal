using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Tests
{
    public class TestHelper
    {
        public User SeedUser()
        {
            return new User
            {
                UserId = 1,
                Address = "Test",
                Birthday = DateTime.Now.Date,
                Email = "testEmail",
                FirstName = "Test",
                LastName = "Person",
                TelephoneNumber = "0612345678",
                Diabetes = false,
                GlutenAllergy = false,
                Saltless = false
            };
        }

        public Chef SeedChef()
        {
            return new Chef
            {
                ChefId = 1,
                Email = "Test@chef.com",
                FirstName = "Test",
                LastName = "Chef",
                TelephoneNumber = "0612345678"
            };
        }

        public Meal SeedMeal()
        {
            return new Meal
            {
                MealId = 1,
                ChefId = 1,
                Appetizer = "Salad",
                MainDish = "Pizza",
                Dessert = "Icecream",
                Date = DateTime.Now.Date,
                DayOfTheWeek = DateTime.Now.DayOfWeek.ToString(),
                Description = "Standard food",
                Diabetes = true,
                GlutenAllergy = true,
                ImageData = null,
                Name = "Pizza time",
                Price = 7.00M,
                Saltless = false
            };
        }

        public IList<Order> SeedOrders(int amount)
        {
            User user = SeedUser();
            IList<Order> orderList = new List<Order>();

            DateTime startDate = new DateTime(2021, 1, 10);

            for (int i = 0; i < amount; i++)
            {
                Order order = new Order
                {
                    OrderId = i,
                    MealId = 1,
                    Customer = user,
                    Appetizer = true,
                    Dessert = true,
                    Date = startDate.AddDays(i),
                    Day = startDate.AddDays(i).DayOfWeek,
                    OrderSize = Size.Large,
                    Price = 7.00M
                };

                orderList.Add(order);
            }

            return orderList;
        }
    }
}
