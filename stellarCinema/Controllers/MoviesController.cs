using Microsoft.AspNetCore.Mvc;

namespace stellarCinema.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
