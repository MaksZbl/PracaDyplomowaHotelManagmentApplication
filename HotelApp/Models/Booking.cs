using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApp.Models
{
    public class Booking
    {
        [Key]
        public int Booking_id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("Customer")]
        public int? Customer_id { get; set; }
    }
}
