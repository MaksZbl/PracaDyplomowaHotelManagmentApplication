using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApp.Models
{
    public class Rate
    {
        [Key]
        public int RateId { get; set; }
        [Required]
        public double value { get; set; }
        public virtual LoggedInUser LoggedInUser { get; set; }
        [ForeignKey("LoggedInUser")]
        public int? LoggedInUserId { get; set; }
        public virtual Hotel Hotel { get; set; }
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }
    }
}
