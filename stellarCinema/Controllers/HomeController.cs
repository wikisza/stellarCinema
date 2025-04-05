using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stellarCinema.Entities;
using stellarCinema.Interfaces;
using stellarCinema.Models;
using static stellarCinema.Models.UserReservationViewModel;

namespace stellarCinema.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IReservationService _reservationService;  
        private readonly ILogger<HomeController> _logger;

        
        public HomeController(ILogger<HomeController> logger, ApplicationContext context, IReservationService reservationService)
        {
            _logger = logger;
            _context = context;
            _reservationService = reservationService;  
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();
            return View(movies);
        }

        public IActionResult EmployeePanel()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CustomerView()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var model = new UserReservationsViewModel
            {
                CurrentReservations = _reservationService.GetCurrentReservationsForUser(email),
                HistoryReservations = _reservationService.GetHistoryReservationsForUser(email)
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
