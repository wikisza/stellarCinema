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
                AddSeatsForHall(hall);
                
                return RedirectToAction(nameof(Index));

            }
            return View(hall);
        }

        private void AddSeatsForHall(Hall hall)
        {
            int totalSeats = hall.TotalSeats;

            int rowCount = (int)Math.Ceiling(totalSeats / 10.0);
            List<Seat> seats = new List<Seat>();

            for (int row = 0; row < rowCount; row++)
            {
                char rowLetter = (char)('A' + row); 
                for (int seatNumber = 1; seatNumber <= 10 && (row * 10 + seatNumber) <= totalSeats; seatNumber++)
                {
                    seats.Add(new Seat
                    {
                        SeatNumber = $"{rowLetter}{seatNumber}", 
                        IdHall = hall.IdHall
                    });
                }
            }

            _context.Seats.AddRange(seats);
            _context.SaveChanges();
        }

    }
}
