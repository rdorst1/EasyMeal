using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        public bool Appetizer { get; set; }
        public bool Dessert { get; set; }
        public Size OrderSize { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price
        {
            get; set;
            //if (orderSize == Size.Small)
            //{
            //    return decimal.Multiply(price, (decimal)0.8);
            //}
            //else if (orderSize == Size.Large)
            //{
            //    return decimal.Multiply(price, (decimal)1.2);
            //}
            //else
            //{
            //    return price;
            //}
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
