using System;
using System.Collections.Generic;
using System.Text;

namespace EasyMeal.Domain.Models
{
    public class Invoice
    {
        private decimal _totalPrice;
        public int Id { get; set; }
        public User Customer { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice {
            get {
                if (Orders != null)
                {
                    var amountOfOrders = Orders.Count;

                    foreach (var or in Orders)
                    {
                        _totalPrice += or.Price;
                        if (CheckBirthday(or))
                        {
                            _totalPrice -= or.Price;
                            Birthday = true;
                            amountOfOrders -= 1;
                        }
                    }

                    if (amountOfOrders >= 15)
                    {
                        FifteenOrdersOrMore = true;
                        _totalPrice = decimal.Multiply(_totalPrice, (decimal)0.9);
                    }
                }

                return _totalPrice;
            }

            set {
                _totalPrice = value;
            }
        }

        private bool CheckBirthday(Order or)
        {
            if(or.Date.Date.Month == Customer.Birthday.Date.Month && or.Date.Date.Day == Customer.Birthday.Date.Day)
            {
                return true;
            }
            return false;
        }

        public bool Birthday { get; set; }
        public bool FifteenOrdersOrMore { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
