using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    public class Hall
    {
        [Key]
        public int IdHall { get; set; }
        [MaxLength(5)]
        public string HallName { get; set; }
        public int TotalSeats { get; set; }


        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

    }
}
