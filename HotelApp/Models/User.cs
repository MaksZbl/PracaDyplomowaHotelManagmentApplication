using System.ComponentModel.DataAnnotations;
using System;

namespace HotelApp.Models
{
    public class User
    {
        [Key]
        public int User_id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }

    }
}
