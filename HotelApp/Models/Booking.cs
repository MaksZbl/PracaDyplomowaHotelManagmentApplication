using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApp.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public virtual LoggedInUser LoggedInUser { get; set; }
        [ForeignKey("LoggedInUser")]
        public int? Customer_id { get; set; }

        public virtual Room Room { get; set; }
        [ForeignKey("Room")]
        public int? RoomId { get; set; }

    }
}
