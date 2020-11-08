
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace EasyMealOrder.Models.ViewModels
{
    public class MealViewModel
    {
        [DataMember(Name = "mealId")]
        public int MealId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "appetizer")]
        public string Appetizer { get; set; }
        [DataMember(Name = "mainDish")]
        public string MainDish { get; set; }
        [DataMember(Name = "dessert")]
        public string Dessert { get; set; }
        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
        [DataMember(Name = "saltless")]
        public bool Saltless { get; set; }
        [DataMember(Name = "diabetes")]
        public bool Diabetes { get; set; }
        [DataMember(Name = "glutenAllergy")]
        public bool GlutenAllergy { get; set; }
        [DataMember(Name = "chefId")]
        public int ChefId { get; set; }
        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }
        [DataMember(Name = "firstName")]
        public string LastName { get; set; }
    }
}
