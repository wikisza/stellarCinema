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
        public async Task<IActionResult> Book([Bind("IdShowtime, IdReservation,Email, IdHall, TotalSeats, IdPayment, TotalPrice, SeatsList")] BookingViewModel bookingView)
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
            string[] strings = bookingView.SeatsList.Split(",");

            foreach (string seatId in strings)
            {

                ReservationSeat newReservationSeat = new ReservationSeat
                {
                    IdShowtime = bookingView.IdShowtime,
                    IdReservation = newReservation.IdReservation,
                    IdSeat = Int32.Parse(seatId)
                };
                _context.ReservationSeats.Add(newReservationSeat);
                await _context.SaveChangesAsync();
            };


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
                TotalPrice = totalAmount,
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
            if (existingReservation == null)
            {
                return NotFound();
            }
            existingReservation.Status = "Confirmed";

            _context.Payments.Add(newPayment);
            await _context.SaveChangesAsync();

            
            return RedirectToAction("FinalView", bookingView);
        }

        public IActionResult FinalView(BookingViewModel bookingView)
        {
            return View(bookingView);
        }

        public async Task<IActionResult> Download(int IdReservation, decimal TotalPrice)
        {
            var thisReservation = _context.Reservations
                .FirstOrDefault(r => r.IdReservation == IdReservation);

            if (thisReservation == null)
            {
                return NotFound();
            }

            var movie = await _context.Showtimes
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(r => r.IdShowtime == thisReservation.IdShowtime);

            if (movie == null)
            {
                return NotFound();
            }

            var seats = _context.ReservationSeats
                .Where(r => r.IdReservation == thisReservation.IdReservation && r.IdShowtime == thisReservation.IdShowtime)
                .Include(r => r.Seat)
                .Select(r => r.Seat.SeatNumber)
                .ToArray();


            return DownloadTicket(movie.Movie.Title, movie.ShowtimeDateStart, TotalPrice, seats);
        }


        public IActionResult DownloadTicket(string movieTitle, DateTime movieDate, decimal Price, string[] Seats)
        {

            var filePath = Path.Combine(Path.GetTempPath(), "bilet.pdf");
            PdfGenerator.GenerateTicket(movieTitle, movieDate, Price, Seats, filePath);

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", "bilet.pdf");
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

        public IActionResult GetTakenSeats(int IdShowtime)
        {
            var SeatsId = _context.ReservationSeats
                .Where(s => s.IdShowtime == IdShowtime)
                .Select(s => s.IdSeat)
                .ToList();
            return Json(SeatsId);
        }

        private bool ReservationExist(int id)
        {
            return _context.Reservations.Any(r => r.IdReservation == id);
        }




    }
}
