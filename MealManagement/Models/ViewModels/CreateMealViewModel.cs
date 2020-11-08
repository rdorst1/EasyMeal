using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MealManagement.Models.ViewModels
{
    public class CreateMealViewModel
    {
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        [Required(ErrorMessage = "Please enter an Appetizer")]
        public string Appetizer { get; set; }
        [Required(ErrorMessage = "Please enter a Main dish")]
        public string MainDish { get; set; }
        [Required(ErrorMessage = "Please enter a Dessert")]
        public string Dessert { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Remote("ValidateDate", "Meal")]
        public DateTime Date { get; set; }
        public bool Saltless { get; set; }
        public bool Diabetes { get; set; }
        public bool GlutenAllergy { get; set; }
        [Required(ErrorMessage = "Please enter a Chef")]
        public int ChefId { get; set; }
    }
}
