using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stellarCinema.Entities;

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
            var showtime = await _context.Showtimes
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(s => s.IdShowtime == id);



            return View(showtime);
        }


    }
}
