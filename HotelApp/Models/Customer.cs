using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class Customer:LoggedInUser
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Adress { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Mobile { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
