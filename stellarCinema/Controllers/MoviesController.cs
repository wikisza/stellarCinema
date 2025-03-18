using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> GetNowPlaying()
        {
            var nowPlayingMovies = await _context.Movies
                .Where(m => m.ReleaseDate <= DateOnly.FromDateTime(DateTime.Today))
                .ToListAsync();
            return Json(nowPlayingMovies);
        }

        public async Task<IActionResult> GetComingSoon()
        {
            var comingSoonMovies = await _context.Movies
                .Where(m => m.ReleaseDate > DateOnly.FromDateTime(DateTime.Today))
                .ToListAsync();
            return Json(comingSoonMovies);
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
                    Directory.CreateDirectory(uploadsFolder); // Tworzy folder, jeśli nie istnieje

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

    }
}
