using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
