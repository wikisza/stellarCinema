using static stellarCinema.Models.UserReservationViewModel;

namespace stellarCinema.Interfaces
{
    public interface IReservationService
    {
        List<ReservationViewModel> GetCurrentReservationsForUser(string username);
        List<ReservationViewModel> GetHistoryReservationsForUser(string username);
    }
}
