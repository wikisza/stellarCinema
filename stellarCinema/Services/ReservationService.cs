using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using stellarCinema.Entities;
using stellarCinema.Interfaces;
using stellarCinema.Models;

namespace stellarCinema.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationContext _context;
        public ReservationService(ApplicationContext context)
        {
            _context = context;
        }
        public List<UserReservationViewModel.ReservationViewModel> GetCurrentReservationsForUser(string email)
        { 

            var model = _context.Reservations
                .Where(r => r.Email == email && r.ReservationDate >= DateTime.Now)
                .Include(r => r.Showtime)
                .Select(r => new UserReservationViewModel.ReservationViewModel
                {
                    IdReservation = r.IdReservation,
                    Email = r.Email,
                    IdShowtime = r.IdShowtime,
                    ReservationDate = r.ReservationDate,
                    MovieTitle = r.Showtime.Movie.Title,
                    ShowtimeDate = r.Showtime.ShowtimeDateStart,

                    SeatsList = _context.ReservationSeats
                        .Where(rs => rs.IdReservation == r.IdReservation)
                        .Select(rs => rs.Seat.SeatNumber)
                        .ToArray(),

                    TotalPrice = _context.Payments
                        .Where(p => p.IdReservation == r.IdReservation)
                        .Select(p => p.Amount)
                        .FirstOrDefault()

                    }).ToList();

            return model;
        }

        public List<UserReservationViewModel.ReservationViewModel> GetHistoryReservationsForUser(string email)
        {
            var model = _context.Reservations
                .Where(r => r.Email == email && r.ReservationDate < DateTime.Now)
                .Select(r => new UserReservationViewModel.ReservationViewModel
                {
                    IdReservation = r.IdReservation,
                    Email = r.Email,
                    IdShowtime = r.IdShowtime,
                    ReservationDate = r.ReservationDate,
                    MovieTitle = r.Showtime.Movie.Title,
                    ShowtimeDate = r.Showtime.ShowtimeDateStart,

                    SeatsList = _context.ReservationSeats
                        .Where(rs => rs.IdReservation == r.IdReservation)
                        .Select(rs => rs.Seat.SeatNumber)
                        .ToArray(),

                    TotalPrice = _context.Payments
                        .Where(p => p.IdReservation == r.IdReservation)
                        .Select(p => p.Amount)
                        .FirstOrDefault()

                }).ToList();

            return model;
        }
    }
}
