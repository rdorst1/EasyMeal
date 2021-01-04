using Microsoft.VisualStudio.TestTools.UnitTesting;
using EasyMealOrder.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using EasyMealOrder.Models.Repositories;
using EasyMealOrder.Models.ViewModels.OrderViewModel;
using EasyMealOrder.Models;
using Microsoft.EntityFrameworkCore;

namespace EasyMealOrder.Controllers.Tests
{
    [TestClass()]
    public class OrderControllerTests
    {
        private IOrderRepository orderRepository;
        private IWeekOrderRepository weekOrderRepository;
        private IUserRepository userRepository;
        private EasyMealDbContext context;
        private DbContextOptions<EasyMealDbContext> options;

        public OrderControllerTests()
        {
            options = new DbContextOptions<EasyMealDbContext>();
            context = new EasyMealDbContext(options);
            orderRepository = new EFOrderRepository(context);
            userRepository = new EFUserRepository(context);
            weekOrderRepository = new EFWeekOrderRepository(context, userRepository);
        }

        [TestMethod()]
        public void CreateTest()
        {
            OrderController orderController = new OrderController(orderRepository, weekOrderRepository, userRepository);

            User user = new User
            {
                Email = "testUser"
            };

            userRepository.InsertUser(user);

            CreateModel model = new CreateModel
            {
                Appetizer = false,
                Dessert = true,
                Day = DateTime.Now.DayOfWeek,
                Date = DateTime.Now,
                MealId = 1,
                OrderSize = Size.Large,
                Price = (decimal)15.00,
                Customer = user
            };

            orderController.Create(model);

            Assert.IsNotNull(orderRepository.GetOrderById(model.OrderId));

            orderRepository.DeleteOrder(model.OrderId);
        }
    }
}