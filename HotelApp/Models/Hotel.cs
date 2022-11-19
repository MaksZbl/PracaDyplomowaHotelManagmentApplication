using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace HotelApp.Models
{
    public class Hotel
    {
        [Key]
        public int Hotel_id { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Title { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Address { get; set; }
        public double Rating { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual List<HotelImage> images { get; set; }

    }
}
