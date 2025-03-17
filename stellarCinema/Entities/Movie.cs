using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    public class Movie
    {
        [Key]
        public int IdMovie { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        [MaxLength(100)]
        public string Genre { get; set; }
        [MaxLength(100)]
        public string PosterUrl { get; set; }
        public DateOnly ReleaseDate { get; set; }
        [MaxLength(200)]
        public string MovieLink { get; set; }

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
