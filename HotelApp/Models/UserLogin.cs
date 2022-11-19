using System.ComponentModel.DataAnnotations;

namespace HotelApp.Models
{
    public class UserLogin
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} powinno zawierać min {2} i max {1}", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
