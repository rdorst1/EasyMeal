using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyMeal.Domain.Interfaces;
using EasyMeal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyMealOrder.Controllers
{
    public class InvoiceController : Controller
    {
        private static IInvoiceRespository invoiceRepository;
        private IUserRepository userRepository;

        public InvoiceController(IInvoiceRespository invoiceRepo, IUserRepository userRepo)
        {
            invoiceRepository = invoiceRepo;
            userRepository = userRepo;
        }

        // GET: InvoiceController
        public ActionResult Index()
        {
            var currentUser = userRepository.GetUserByEmail(User.Identity.Name);
            ViewBag.Invoices = invoiceRepository.GetInvoices(currentUser.UserId).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateTime MonthYear)
        {
            if (ModelState.IsValid)
            {
                if(MonthYear > DateTime.Now.Date)
                {
                    ModelState.AddModelError("MonthYear", "You can not make an invoice of the future!");
                    ViewBag.Invoices = null;
                    return View();
                }else if (MonthYear.Month == DateTime.Now.Month)
                {
                    ModelState.AddModelError("MonthYear", "You can not make an invoice of this month yet!");
                    ViewBag.Invoices = null;
                    return View();
                }else
                {
                    var currentUser = userRepository.GetUserByEmail(User.Identity.Name);

                    invoiceRepository.CreateInvoice(currentUser.UserId, MonthYear);
                    return RedirectToAction("Index");
                }
            }
            else
            {
                ModelState.AddModelError("MonthYear", "Unable to create Invoice");
                return View();
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            invoiceRepository.DeleteInvoice(id);
            return RedirectToAction("Index");
        }
    }
}
