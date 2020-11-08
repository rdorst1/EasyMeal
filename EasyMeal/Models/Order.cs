using EasyMealOrder.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMealOrder.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public bool Appetizer { get; set; }
        public bool Dessert { get; set; }
        public Size OrderSize { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public User Customer { get; set; }
        public DayOfWeek Day { get; set; }
        public WeekOrder WeekOrder { get; set; }
    }

    public enum Size
    {
        [Display(Name = "Regular")]
        Medium,
        Small,
        Large
    }
}
