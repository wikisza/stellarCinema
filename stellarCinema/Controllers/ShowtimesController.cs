using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stellarCinema.Entities;

namespace stellarCinema.Controllers
{
    public class ShowtimesController : Controller
    {
        private readonly ApplicationContext _context;
        public ShowtimesController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var showtimes = await _context.Showtimes
                .Include(s =>s.Movie)
                .Include(s=>s.Hall)
            .ToListAsync();

            return View(showtimes);
        }

        public IActionResult Create()
        {
            var halls = _context.Halls.ToList();
            ViewBag.Halls = halls;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Showtime showtime)
        {
            bool isAvailable = !_context.Showtimes
                .Any(s => s.IdHall == showtime.IdHall && s.ShowtimeDate == showtime.ShowtimeDate);

            if (!isAvailable)
            {
                ModelState.AddModelError("ShowtimeDate", "Ta sala jest już zajęta w wybranym terminie.");
                ViewBag.Halls = _context.Halls.ToList();
                return View(showtime);
            }

            if (ModelState.IsValid)
            {
                _context.Showtimes.Add(showtime);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Halls = _context.Halls.ToList();
            return View(showtime);
        }




    }
}
