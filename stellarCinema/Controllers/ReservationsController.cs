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

            

            return View(showtime);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizeReservation(int idShowtime, string selectedSeats)
        {
            if (string.IsNullOrEmpty(selectedSeats))
            {
                return RedirectToAction("Index"); 
            }

            

            return View();
        }
        public IActionResult GetSeatPrice()
        {
            var seatPrice = _context.Configurations.Where(s => s.Id == 1).Select(s => s.KeyValue).FirstOrDefault();

            return Json(seatPrice);
        }
        public IActionResult ConfirmBooking()
        {
            return View();
        }




    }
}
