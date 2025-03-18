using Microsoft.AspNetCore.Mvc;

namespace stellarCinema.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
