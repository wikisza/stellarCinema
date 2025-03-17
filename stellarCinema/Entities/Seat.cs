using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    public class Seat
    {
        [Key]
        public int IdSeat { get; set; }
        public int IdHall { get; set; }
        public Hall? Hall { get; set; }
        [MaxLength(10)]
        public string SeatNumber { get; set; }

        public ICollection<ReservationSeat> ReservationSeats { get; set; } = new List<ReservationSeat>();
    }
}
