using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace HotelApp.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Payment_id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CardOwnerName { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string ExpirationDate { get; set; }
        public DateTime PaymentDate { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string SecurityCode { get; set; }
        public decimal ValueOfPayment { get; set; }
        public virtual Booking Booking { get; set; }
        [ForeignKey("Booking")]
        public int? Booking_id { get; set; }
    }
}
