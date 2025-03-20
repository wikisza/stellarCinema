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
            .ToListAsync();

            return View(showtimes);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
