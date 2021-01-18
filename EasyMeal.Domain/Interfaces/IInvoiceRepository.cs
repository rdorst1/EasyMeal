using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMeal.Domain.Interfaces
{
    public interface IInvoiceRespository
    {
        Invoice GetSpecificInvoice(int userId, string month);
        IQueryable<Invoice> GetInvoices(int userId);
        public void CreateInvoice(Invoice invoice);
    }
}
