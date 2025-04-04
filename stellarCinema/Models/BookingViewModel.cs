using stellarCinema.Entities;

namespace stellarCinema.Models
{
    public class BookingViewModel
    {
        public int IdShowtime { get; set; }
        public Showtime? Showtime { get; set; }
        public int IdReservation {  get; set; }
        public Reservation? Reservation { get; set; }
        public string Email { get; set; }
        public int IdHall { get; set; }
        public Hall? Hall { get; set; }
        public int TotalSeats { get; set; }
        public int IdPayment { get; set; }
        public Payment? Payment { get; set; }
        public decimal TotalPrice { get; set; }
        public string SeatsList { get; set; }
        
        public ReservationSeat? ReservationSeat { get; set; }
    }
}
