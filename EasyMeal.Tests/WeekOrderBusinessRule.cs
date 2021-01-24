using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMeal.Tests
{
    public class WeekOrderBusinessRule
    {
        public readonly IWeekOrderRepository MockWORepository;
        public TestHelper _testHelper;

        public WeekOrderBusinessRule()
        {
            TestHelper testHelper = new TestHelper();
            _testHelper = testHelper;
            Mock<IWeekOrderRepository> mockWORepository = new Mock<IWeekOrderRepository>();

            User user = _testHelper.SeedUser();
            IList<Order> orders = _testHelper.SeedOrders(5);

            DateTime startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);

            IList<WeekOrder> wos = new List<WeekOrder>
            {
                new WeekOrder
                {   
                    WeekOrderId = 1,
                    Customer = user,
                    Orders = orders,
                    StartDate = startOfWeek,
                    EndDate = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59)
                }
            };

            mockWORepository.Setup(mw => mw.GetWeekOrders()).Returns(wos);

            mockWORepository.Setup(mw => mw.GetWeekOrderById(It.IsAny<int>())).Returns((int i) => wos.Where(
                x => x.WeekOrderId == i).FirstOrDefault());

            MockWORepository = mockWORepository.Object;
        }

        [Test]
        public void CanReturnWeekOrderById()
        {
            WeekOrder wo = MockWORepository.GetWeekOrderById(1);

            Assert.IsNotNull(wo);
            Assert.IsInstanceOf(typeof(WeekOrder), wo);
        }

        [Test]
        public void OrderForAtleastFourWorkDays()
        {
            WeekOrder wo = MockWORepository.GetWeekOrderById(1);

            Assert.IsTrue(wo.CanCompleteOrder);
            Assert.IsTrue(wo.Orders.Count >= 4);
        }

        [Test]
        public void WeekOrderPriceWithoutDiscounts()
        {
            WeekOrder weekOrder = MockWORepository.GetWeekOrderById(1);

            decimal woPrice = weekOrder.TotalPrice;

            decimal calculatedPrice = 0;

            foreach(Order or in weekOrder.Orders)
            {
                calculatedPrice += or.Price;
            }

            Assert.AreEqual(calculatedPrice, woPrice);
        }
    }
}
