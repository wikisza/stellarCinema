using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    public class Showtime
    {
        [Key]
        public int IdShowtime { get; set; }
        public int IdMovie { get; set; }
        public Movie? Movie { get; set; }
        public int IdHall { get; set; }
        public Hall? Hall { get; set; }
        [Precision(0)]
        public DateTime ShowtimeDateStart { get; set; }
        [Precision(0)]
        public DateTime ShowtimeDateEnd { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<ReservationSeat> ReservationSeats { get; set; } = new List<ReservationSeat>();
    }
}
