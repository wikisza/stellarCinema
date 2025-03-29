using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using stellarCinema.Entities;

namespace stellarCinema.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationContext _context;

        public MoviesController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies
            .ToListAsync();

            return View(movies);
        }

        [HttpGet("/GetNowPlaying")]
        public async Task<JsonResult> GetNowPlaying()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var nowPlayingMovies = await _context.Movies
                .Where(m => m.ReleaseDate < today)
                .ToListAsync();

            return Json(nowPlayingMovies);
        }

        [HttpGet("/GetComingSoon")]
        public async Task<IActionResult> GetComingSoon()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var comingSoonMovies = await _context.Movies
                .Where(m => m.ReleaseDate > today)
                .ToListAsync();
            return Json(comingSoonMovies);
        }

        [HttpGet]
        public JsonResult GetMovieSuggestions(string term)
        {
            var movies = _context.Movies
                .Where(m => m.Title.Contains(term))
                .Select(m => new { idMovie = m.IdMovie, title = m.Title })
                .Take(10)
                .ToList();

            return Json(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMovie,Title, Description, Duration, Genre, ReleaseDate, MovieLink")]Movie movie, IFormFile PosterFile)
        {
            if (ModelState.IsValid)
            {
                string posterUrl = "";
                if (PosterFile != null && PosterFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "posters");
                    Directory.CreateDirectory(uploadsFolder); 

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PosterFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await PosterFile.CopyToAsync(fileStream);
                    }
                posterUrl = "/posters/" + fileName;

                }

                DateOnly date = DateOnly.FromDateTime(DateTime.Now);

                Movie newMovie = new Movie
                {
                    Title = movie.Title,
                    Description = movie.Description,
                    Duration = movie.Duration,
                    Genre = movie.Genre,
                    ReleaseDate = movie.ReleaseDate,
                    MovieLink = movie.MovieLink,
                    PosterUrl = posterUrl
                };


                _context.Add(newMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.IdMovie == id);
            if (movie == null)
            {
                Console.WriteLine($"Movie with ID {id} not found.");
                return NotFound();
            }

            return View(movie);
        }

        public IActionResult AvailableMovieHours(int id)
        {
            var now = DateTime.Now;
            var showtimes = _context.Showtimes
            .Where(s => s.IdMovie == id && s.ShowtimeDateStart > now)
            .OrderBy(s => s.ShowtimeDateStart)
            .ToList();

            ViewBag.Movie = _context.Movies.FirstOrDefault(m => m.IdMovie == id);
            ViewBag.MovieId = id;

            return View(showtimes);
        }

    }
}
