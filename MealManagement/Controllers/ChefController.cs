using System;
using System.Data;
using System.Linq;
using EasyMeal.Domain.Interfaces;
using EasyMeal.Domain.Models;
using EasyMeal.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MealManagement.Controllers
{
    public class ChefController : Controller
    {
        private IChefRepository chefRepository;

        public ChefController(IChefRepository chefRepo)
        {
            chefRepository = chefRepo;
        }

        [Authorize]
        public IActionResult Index()
        {
            var chefs = from chef in chefRepository.GetChefs()
                        select chef;
            return View(chefs);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View(new Chef());
        }

        [HttpPost]
        public ActionResult Create(Chef chef)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    chefRepository.InsertChef(chef);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save Chef");
            }
            return View(chef);
        }

        [Authorize]
        public ActionResult Update(int id)
        {
            Chef chef = chefRepository.GetChefByID(id);
            return View(chef);
        }

        [HttpPost]
        public ActionResult Update(Chef chef)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var id = RouteData.Values["id"].ToString();
                    chefRepository.UpdateChef(Int32.Parse(id), chef);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to update Chef");
            }
            return View(chef);
        }

        [Authorize]
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Please try again.";           
            }
            Chef chef = chefRepository.GetChefByID(id);
            return View(chef);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Chef chef = chefRepository.GetChefByID(id);
                chefRepository.DeleteChef(id);
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                        new Microsoft.AspNetCore.Routing.RouteValueDictionary
                        {
                            { "id", id },
                            { "saveChangesError", true }
                        });
            }
            return RedirectToAction("Index");
        }
    }
}