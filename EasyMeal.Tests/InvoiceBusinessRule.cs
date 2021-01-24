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
    public class InvoiceBusinessRule
    {
        public readonly IInvoiceRespository MockInvoiceRepository;
        public TestHelper _testHelper;
        public InvoiceBusinessRule()
        {
            TestHelper testHelper = new TestHelper();
            _testHelper = testHelper;
            Mock<IInvoiceRespository> mockInvoiceRepository = new Mock<IInvoiceRespository>();

            User user = _testHelper.SeedUser();
            Chef chef = _testHelper.SeedChef();
            Meal meal = _testHelper.SeedMeal();
            IList<Order> orders = _testHelper.SeedOrders(16);

            IList<Invoice> invoices = new List<Invoice>
            {
                new Invoice
                {
                    Id = 1,
                    Customer = user,
                    Date = DateTime.Now.Date,
                    Orders = orders
                }
            };

            // Get all invoices
            mockInvoiceRepository.Setup(mr => mr.GetInvoices(It.IsAny<int>())).Returns(invoices);

            // Get invoice based on userId and month
            mockInvoiceRepository.Setup(mr =>
                mr.GetSpecificInvoice(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((int i, int j) =>
                    invoices.Where(x =>
                        x.Customer.UserId == i && x.Orders.All(m
                            => m.Date.Month == j)).FirstOrDefault());

            MockInvoiceRepository = mockInvoiceRepository.Object;
        }

        [Test]
        public void ReturnAllInvoices()
        {
            IList<Invoice> testInvoices = MockInvoiceRepository.GetInvoices(1);

            Assert.IsNotNull(testInvoices);
        }

        [Test]
        public void MealOnBirthDayIsFree()
        {
            Invoice invoice = MockInvoiceRepository.GetSpecificInvoice(1, 1);
            var birthday = invoice.Customer.Birthday;
            decimal orderTotalPrice = 0;
            decimal invoiceTotalPrice = invoice.TotalPrice;

            foreach (var or in invoice.Orders)
            {
                orderTotalPrice += or.Price;
            };

            Assert.AreNotEqual(orderTotalPrice, invoiceTotalPrice);
            Assert.IsTrue(invoice.Orders.Any(x => x.Date == birthday));
            Assert.IsTrue(invoice.Birthday);
        }

        [Test]
        public void FifteenOrMoreMealsTenProcentDiscount()
        {
            Invoice invoice = MockInvoiceRepository.GetSpecificInvoice(1, 1);
            decimal invoiceTotalPrice = invoice.TotalPrice;
            decimal orderTotalPrice = 0;

            foreach(var or in invoice.Orders)
            {
                orderTotalPrice += or.Price;             
            };

            Assert.AreEqual(invoiceTotalPrice, (orderTotalPrice - 8.4M) * 0.9M);
            Assert.IsTrue(invoice.Orders.Count >= 15);
            Assert.IsTrue(invoice.FifteenOrdersOrMore);
        }
    }
}
