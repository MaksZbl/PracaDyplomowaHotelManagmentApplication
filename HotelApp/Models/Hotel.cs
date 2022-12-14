using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace HotelApp.Models
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Hotel_id { get; set; }
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Title { get; set; }
        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public string Description { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Type { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Address { get; set; }
        public virtual List<Rate> Rates { get; set; }
        public virtual List<LoggedInUser> Employees { get; set; }
        public virtual List<Room> Rooms { get; set; }
        public virtual List<HotelImage> images { get; set; }

    }
}
