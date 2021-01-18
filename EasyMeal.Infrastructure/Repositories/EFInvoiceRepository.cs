using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
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

        public void CreateInvoice(Invoice invoice)
        {
            
        }

        public IQueryable<Invoice> GetInvoices(int userId)
        {
            return context.Invoices.Where(x => x.Customer.UserId == userId);
        }

        public Invoice GetSpecificInvoice(int userId, string month)
        {
            throw new NotImplementedException();
        }
    }
}
