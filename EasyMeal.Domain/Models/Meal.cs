using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string Appetizer { get; set; }
        public string MainDish { get; set; }
        public string Dessert { get; set; }
        public DateTime Date { get; set; }
        public string DayOfTheWeek { get; set; }
        public bool Saltless { get; set; }
        public bool Diabetes { get; set; }
        public bool GlutenAllergy { get; set; }
        public int ChefId { get; set; }
    }
}
