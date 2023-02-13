using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Eventing.Reader;

namespace HotelApp.Models
{
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Room_id { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1)]
        public string Number { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 5)]
        public string Description { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Type { get; set; }
        public virtual Hotel Hotel { get; set; }
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }
        public bool IsFree { get; set; }
        public double Rent { get; set; }
        public virtual List<RoomImage> images { get; set; }
        public virtual Booking Booking { get; set; }
        
        [ForeignKey("Booking")]
        public int? BookingId { get; set; }
        
    }
}
