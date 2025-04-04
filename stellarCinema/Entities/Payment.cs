using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    public class Payment
    {
        [Key]
        public int IdPayment { get; set; }
        public int IdReservation { get; set; }
        public Reservation? Reservation { get; set; }
        [Precision(2)]
        public DateTime PaymentDate { get; set; }
        [Precision(10, 2)]
        public decimal Amount { get; set; }
        [MaxLength(30)]
        public string Status { get; set; }

        
    }
}
