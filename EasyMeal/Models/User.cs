using System;

namespace EasyMealOrder.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime Birthday { get; set; }
        public bool Saltless { get; set; }
        public bool Diabetes { get; set; }
        public bool GlutenAllergy { get; set; }

    }
}
