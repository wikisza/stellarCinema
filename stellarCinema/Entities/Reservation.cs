using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    public class Reservation
    {
        [Key]
        public int IdReservation { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public int IdShowtime { get; set; }
        public Showtime? Showtime { get; set; }
        [Precision(2)]
        public DateTime ReservationDate { get; set; }
        [MaxLength(30)]
        public string Status { get; set; }
        public Payment? Payment { get; set; }

        public ICollection<ReservationSeat> ReservationSeats { get; set; } = new List<ReservationSeat>();
    }
}
