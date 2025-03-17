namespace stellarCinema.Entities
{
    public class ReservationSeat
    {
        public int Id { get; set; }
        public int IdReservation { get; set; }
        public Reservation? Reservation { get; set; }
        public int IdSeat { get; set; }
        public Seat? Seat { get; set; }

    }
}
