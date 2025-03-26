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
        public IActionResult Index()
        {
            return View();
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
            var movie = _context.Movies.FirstOrDefault(m => m.IdMovie == showtime.IdMovie);
            if (movie == null)
            {
                ModelState.AddModelError("IdMovie", "Nie znaleziono wybranego filmu.");
                ViewBag.Halls = _context.Halls.ToList();
                return View(showtime);
            }

            showtime.ShowtimeDateEnd = showtime.ShowtimeDateStart.AddMinutes(movie.Duration.TotalMinutes);

            bool isAvailable = !_context.Showtimes.Any(s =>
                s.IdHall == showtime.IdHall &&
                (showtime.ShowtimeDateStart < s.ShowtimeDateEnd && showtime.ShowtimeDateEnd > s.ShowtimeDateStart)
            );

            if (!isAvailable)
            {
                ModelState.AddModelError("ShowtimeDateStart", "Sala jest już zajęta w wybranym terminie.");
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



        [HttpGet("/get_showtimes")]
        public IActionResult GetShowtimes(int id)
        {
            var now = DateTime.Now;

            var showtimes = _context.Showtimes
                .Where(s => s.IdMovie == id)
                .Include(s => s.Movie)  
                .Include(s => s.Hall)  
                .Select(s => new
                {
                    id = s.IdShowtime,
                    start = s.ShowtimeDateStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    end = s.ShowtimeDateEnd.ToString("yyyy-MM-dd HH:mm:ss"),  
                    title = s.Movie.Title,
                    description = " Sala numer " + s.Hall.HallName,
                    isPast = s.ShowtimeDateStart <= now
                })
                .ToList();

            return Json(showtimes);  
        }

        [HttpGet("/get_all_showtimes")]
        public IActionResult GetAllShowtimes()
        {
            var now = DateTime.Now;
            var showtimes = _context.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.Hall)
                .Select(s => new
                {
                    id = s.IdShowtime,
                    start = s.ShowtimeDateStart.ToString("yyyy-MM-dd HH:mm:ss"),
                    end = s.ShowtimeDateEnd.ToString("yyyy-MM-dd HH:mm:ss"),
                    title = s.Movie.Title,
                    description = s.Hall.HallName,
                    isPast = s.ShowtimeDateStart <= now
                })
                .ToList();

            return Json(showtimes);
        }




    }
}
