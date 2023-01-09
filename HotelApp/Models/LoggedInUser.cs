using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApp.Models
{
    public class LoggedInUser: User
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} powinno zawierać min {2} i max {1}", MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        public string RoleValue { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Adress { get; set; }
        public virtual Hotel Hotel { get; set; }
        [ForeignKey("Hotel")]
        public int? HotelId { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Mobile { get; set; }
        public virtual List<Payment> Payments { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<Rate> Rates { get; set; }
    }
}
