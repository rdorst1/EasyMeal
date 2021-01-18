using EasyMeal.Domain.Models;
using EasyMeal.Infrastructure;
using Moq;
using NUnit.Framework;
using System;

namespace EasyMeal.Tests
{
    public class OrderMeal
    {
        private IUserRepository MockUserRepository;
        private IOrderRepository MockOrderRepository;
        private IMealRepository MockMealRepository;
        private IChefRepository MockChefRepository;
        [SetUp]
        public void Setup()
        {
            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            Mock<IOrderRepository> orderRepository = new Mock<IOrderRepository>();
            Mock<IMealRepository> mealRepository = new Mock<IMealRepository>();
            Mock<IChefRepository> chefRepository = new Mock<IChefRepository>();

            User user = new User
            {
                UserId = 1,
                Address = "testStreet",
                Birthday = new DateTime(2000, 4, 5),
                Diabetes = false,
                GlutenAllergy = false,
                Saltless = true,
                Email = "test@email.com",
                FirstName = "John",
                LastName = "Doe",
                TelephoneNumber = "0612345678"
            };

            userRepository.Setup(m => m.InsertUser(user));

            Chef chef = new Chef
            {
                ChefId = 1,
                Email = "testChef@email.com",
                FirstName = "Gordon",
                LastName = "Ramsey",
                TelephoneNumber = "0612345678"
            };

            chefRepository.Setup(m => m.InsertChef(chef));

            Meal meal = new Meal
            {
                MealId = 1,
                Appetizer = "Salad",
                MainDish = "Fries with chicken",
                Dessert = "Dame Blanche",
                ChefId = chef.ChefId,
                Date = new DateTime(),
                DayOfTheWeek = new DateTime().DayOfWeek.ToString(),
                Description = "Standard food",
                Name = "Fries with chicken",
                Diabetes = true,
                GlutenAllergy = false,
                Saltless = false,
                Price = 2.50M,
                ImageData = null
            };

            mealRepository.Setup(m => m.InsertMeal(meal));

            MockUserRepository = userRepository.Object;
            MockChefRepository = chefRepository.Object;
            MockMealRepository = mealRepository.Object;
            MockOrderRepository = orderRepository.Object;
        }

        [Test]
        public void OrderMealWithDietRestriction()
        {
            User testUser = this.MockUserRepository.GetUserById(1);

            Order order = new Order
            {
                OrderId = 1,
                Appetizer = true,
                Dessert = true,
                Customer = testUser,
                Date = new DateTime(),
                Day = new DateTime().DayOfWeek,
                MealId = 1,
                OrderSize = Size.Large,
                Price = 2.50M,
                WeekOrder = null
            };

            this.MockOrderRepository.InsertOrder(order);

            Order testOrder = this.MockOrderRepository.GetOrderById(1);
            Console.WriteLine(testOrder);
        }
    }
}