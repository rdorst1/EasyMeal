using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyMeal.Domain.Interfaces
{
    public interface IInvoiceRespository
    {
        IList<Invoice> GetInvoices(int userId);
        Invoice GetSpecificInvoice(int userId, int month);
        public void CreateInvoice(int userId, DateTime date);
        public void DeleteInvoice(int invoiceId);
    }
}
