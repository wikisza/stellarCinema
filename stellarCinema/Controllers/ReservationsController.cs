using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stellarCinema.Entities;
using stellarCinema.Models;

namespace stellarCinema.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationContext _context;

        public ReservationsController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Book(int id)
        {

            var showtime = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .FirstOrDefault(s => s.IdShowtime == id);

            if (showtime == null) return NotFound();

            var seats = _context.Seats.Where(s => s.IdHall == showtime.IdHall).ToList();

            ViewBag.Seats = seats;

            return View(showtime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int IdShowtime, string Email)
        {
            var now = DateTime.Now;
            string status = "Pending";

            Reservation newReservation = new Reservation
            {
                IdShowtime = IdShowtime,
                Email = Email,
                ReservationDate = now,
                Status = status
            };

            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();
           

            return RedirectToAction(nameof(ConfirmBooking));
        }
        public IActionResult GetSeatPrice()
        {
            var seatPrice = _context.Configurations.Where(s => s.Id == 1).Select(s => s.KeyValue).FirstOrDefault();

            return Json(seatPrice);
        }

        public IActionResult GetSeatId(string seatNumber, int idHall)
        {
            var seatId = _context.Seats
                .Where(s => s.SeatNumber == seatNumber && s.IdHall == idHall)
                .Select(s => s.IdSeat)
                .FirstOrDefault();
            return Json(seatId);
        }
        public IActionResult ConfirmBooking()
        {
            return RedirectToAction("Index", "Home");
        }




    }
}
