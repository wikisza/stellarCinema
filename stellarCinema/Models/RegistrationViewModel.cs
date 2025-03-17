using System.ComponentModel.DataAnnotations;

namespace stellarCinema.Models
{
    public class RegistrationViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w-\.]+)@((\[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Proszę podać poprawny email.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "First name is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        public string? Role { get; set; }
        public string? PhoneNumber { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
    }
}
