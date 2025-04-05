using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace stellarCinema.Models
{
    public class UserReservationViewModel
    {
        public class UserReservationsViewModel
        {
            public List<ReservationViewModel> CurrentReservations { get; set; }
            public List<ReservationViewModel> HistoryReservations { get; set; }
        }

        public class ReservationViewModel
        {
            public int IdReservation { get; set; }
            public string Email { get; set; }
            public int IdShowtime { get; set; }
            public DateTime ReservationDate { get; set; }
            public string MovieTitle { get; set; }
            public DateTime ShowtimeDate { get; set; }
            public string[] SeatsList { get; set; }
            [Precision(10, 2)]
            public decimal TotalPrice { get; set; }
        }
    }
}
