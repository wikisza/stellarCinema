using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stellarCinema.Entities;

namespace stellarCinema.Controllers
{
    public class HallsController : Controller
    {
        private readonly ApplicationContext _context;
        public HallsController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var halls = await _context.Halls
                .ToListAsync();
            return View(halls);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IdHall, HallName, TotalSeats")] Hall hall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hall);
        }
    }
}
