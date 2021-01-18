using EasyMeal.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMealOrder.Models.ViewModels.OrderViewModel
{
    public class CreateModel
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public bool Appetizer { get; set; }
        public bool Dessert { get; set; }
        [Required]
        public Size OrderSize { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public User Customer { get; set; }
        public DayOfWeek Day { get; set; }

    }
}