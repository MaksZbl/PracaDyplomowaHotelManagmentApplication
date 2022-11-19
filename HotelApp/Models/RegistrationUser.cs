using System.ComponentModel.DataAnnotations;
using System;

namespace HotelApp.Models
{
    public class RegistrationUser
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "{0} powinno zawierać min {2} i max {1}", MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAdress { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
