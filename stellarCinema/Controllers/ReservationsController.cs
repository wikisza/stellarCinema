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
            .Where(s => s.IdShowtime == id)
            .FirstOrDefault();

            if (showtime == null) return NotFound();


            var bookingViewModel = new BookingViewModel
            {
                Showtime = showtime,
                IdShowtime = showtime.IdShowtime,
                TotalSeats = showtime.Hall.TotalSeats,
                IdHall = showtime.IdHall
            };

            return View(bookingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book([Bind("IdShowtime, IdReservation,Email, IdHall, TotalSeats, IdPayment, TotalPrice")]BookingViewModel bookingView)
        {
            var now = DateTime.Now;
            string status = "Pending";

            var totalAmount = Convert.ToDecimal(bookingView.TotalPrice);


            Reservation newReservation = new Reservation
            {
                IdShowtime = bookingView.IdShowtime,
                Email = bookingView.Email,
                ReservationDate = now,
                Status = status
            };

            _context.Reservations.Add(newReservation);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ConfirmBooking), new { idReservation = newReservation.IdReservation, totalAmount });

        }

        [HttpGet]
        public async Task<IActionResult> ConfirmBooking(int idReservation, decimal totalAmount)
        {
            var reservation = _context.Reservations
            .Include(x => x.Showtime)
            .Where(s => s.IdReservation == idReservation)
            .FirstOrDefault();

            if (reservation == null) return NotFound();


            var bookingViewModel = new BookingViewModel
            {
                Reservation = reservation,
                TotalPrice = totalAmount
            };

            return View(bookingViewModel);
           
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmBooking([Bind("IdShowtime, IdReservation,Email, IdHall, TotalSeats, IdPayment, TotalPrice")] BookingViewModel bookingView)
        {
            var now = DateTime.Now;
            Payment newPayment = new Payment
            {
                IdReservation = bookingView.IdReservation,
                PaymentDate = now,
                Amount = bookingView.TotalPrice,
                Status = "Confirmed"
            };

            var existingReservation = await _context.Reservations.FindAsync(bookingView.IdReservation);
            if(existingReservation == null)
            {
                return NotFound();
            }
            existingReservation.Status = "Confirmed";

            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
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
        
      
        private bool ReservationExist(int id)
        {
            return _context.Reservations.Any(r => r.IdReservation == id);
        }




    }
}
