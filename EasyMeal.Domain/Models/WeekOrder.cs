using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMeal.Domain.Models
{
    public class WeekOrder
    {
        public decimal _totalPrice;
        public bool _canCompleteOrder;
        public int WeekOrderId { get; set; }
        public User Customer { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalPrice {
            get
            {
                if(Orders != null)
                {
                    foreach(var or in Orders)
                    {
                        _totalPrice += or.Price;
                    }
                }
                return _totalPrice;
            }
            set
            {
                _totalPrice = value;
            }
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Completed { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public bool CanCompleteOrder {
            get
            {
                if (Orders != null && Completed != true)
                {
                    int workDayOrders = 0;
                    foreach (var or in Orders)
                    {
                        if (or.Date.DayOfWeek != DayOfWeek.Saturday && or.Date.DayOfWeek != DayOfWeek.Sunday)
                        {
                            workDayOrders++;
                        }
                    }
                    if (workDayOrders >= 4)
                    {
                        _canCompleteOrder = true;
                    }

                    return _canCompleteOrder;
                }
                _canCompleteOrder = false;
                return _canCompleteOrder;
            }
            set
            {
                CanCompleteOrder = value;
            }
        }
    }
}
