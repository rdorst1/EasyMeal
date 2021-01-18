using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Domain.ViewModels
{
    public class MealResult
    {
        public int MealId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Appetizer { get; set; }
        public string MainDish { get; set; }
        public string Dessert { get; set; }
        public DateTime Date { get; set; }
        public bool Saltless { get; set; }
        public bool Diabetes { get; set; }
        public bool GlutenAllergy { get; set; }
        public int ChefId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
