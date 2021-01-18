using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Domain.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public User Customer { get; set; }
        public DateTime StartOfMonth { get; set; }
        public DateTime EndOfMonth { get; set; }
        public string Month { get; set; }
        public decimal TotalPrice { 
            get
            {
                var amountOfOrders = Orders.Count;

                foreach (var or in Orders)
                {
                    TotalPrice += or.Price;
                    if(or.Date.Date == Customer.Birthday.Date)
                    {
                        TotalPrice -= or.Price;
                        Birthday = true;
                        amountOfOrders -= 1;
                    }
                }
                
                if(amountOfOrders >= 15)
                {
                    TotalPrice = decimal.Multiply(TotalPrice, (decimal)0.9);
                }

                return TotalPrice;
            }

            set { TotalPrice = value; }
        }
        public bool Birthday { get; set; }
        public bool FifteenOrdersOrMore { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<WeekOrder> WeekOrders { get; set; }
    }
}
