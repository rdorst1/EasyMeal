using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain.Models
{
    public class Order
    {
        private decimal _price;
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public bool Appetizer { get; set; }
        public bool Dessert { get; set; }
        public Size OrderSize { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price
        {
            get {
                if (OrderSize == Size.Small)
                {
                    return decimal.Multiply(_price, (decimal)0.8);
                }
                else if (OrderSize == Size.Large)
                {
                    return decimal.Multiply(_price, (decimal)1.2);
                }
                else
                {
                    return _price;
                }
            }
            set {
                _price = value;
            }
        }
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
