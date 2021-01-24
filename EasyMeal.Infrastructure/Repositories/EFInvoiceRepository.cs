using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;

namespace EasyMeal.Infrastructure.Repositories
{
    public class EFInvoiceRepository : IInvoiceRespository
    {
        private EasyMealDbContext context;

        public EFInvoiceRepository(EasyMealDbContext ctx)
        {
            context = ctx;
        }

        public void CreateInvoice(int userId, DateTime date)
        {
            DateTime startOfMonth = new DateTime(date.Year, date.Month, 1);
            DateTime endOfMonth = new DateTime(startOfMonth.Year, startOfMonth.Month, DateTime.DaysInMonth(startOfMonth.Year, startOfMonth.Month));

            var orders = context.Orders.Where(x => x.Customer.UserId == userId &&
                x.Date > startOfMonth && x.Date < endOfMonth
            );
            
            if(orders.Count() > 0)
            {
                Invoice inv = new Invoice
                {
                    Date = DateTime.Now,
                    Customer = context.Users.Find(userId),
                    Orders = orders.ToList()
                };

                context.Invoices.Add(inv);
                context.SaveChanges();
            }
        }

        public void DeleteInvoice(int invoiceId)
        {
            Invoice invoice = context.Invoices.Include(x => x.Orders).Where(x => x.Id == invoiceId).FirstOrDefault();

            invoice.Orders.Clear();
            context.Invoices.Remove(invoice);
            context.SaveChanges();
        }

        public IList<Invoice> GetInvoices(int userId) => context.Invoices.Include(x => x.Orders).Where(x => x.Customer.UserId == userId).ToList();

        public Invoice GetSpecificInvoice(int userId, int month)
        {
            return context.Invoices.Include(x => x.Orders).Where(x => x.Customer.UserId == userId && x.Orders.All(x => x.Date.Month == month)).FirstOrDefault();
        }
    }
}
