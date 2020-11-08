using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyMealOrder.Models
{
    public class WeekOrder
    {
        public int WeekOrderId { get; set; }
        public User Customer { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Completed { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
