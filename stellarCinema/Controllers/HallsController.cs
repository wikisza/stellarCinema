using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .FindAsync(id);

            if (hall == null)
            {
                return NotFound();
            }

            return View(hall);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHall,HallName, TotalSeats")] Hall hall)
        {

            if (id != hall.IdHall)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingHall = await _context.Halls.FindAsync(id);

                    if (existingHall == null)
                    {
                        return NotFound();
                    }
                    existingHall.HallName = hall.HallName;
                    existingHall.TotalSeats = hall.TotalSeats;


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HallExist(hall.IdHall))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Edit), new { id });
        }

        private bool HallExist(int Id)
        {
            return _context.Halls.Any(e => e.IdHall == Id);
        }

    }

}
