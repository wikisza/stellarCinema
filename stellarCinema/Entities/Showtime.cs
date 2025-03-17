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
        public DateTime ShowtimeDate { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
