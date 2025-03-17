using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stellarCinema.Entities;
using stellarCinema.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace stellarCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
                if (ModelState.IsValid)
            {
                var existingUser = _context.UserAccounts.Any(u => u.Email == model.Email);
                if (existingUser)
                {
                    ModelState.AddModelError("", "Email lub nazwa użytkownika już istnieje.");
                    return View(model);
                }
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                DateTime date = DateTime.Now;

                string baseRole = "customer";

                UserAccount account = new UserAccount
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = hashedPassword,
                    PhoneNumber = model.PhoneNumber,
                    Role = baseRole,
                    CreatedAt = date,
                };

                _context.UserAccounts.Add(account);
                _context.SaveChanges();

                ModelState.Clear();
                ViewBag.Message = $"Użytkownika {account.FirstName} {account.LastName} zarejestrowano pomyślnie. Proszę zaloguj się.";
            }

            return View(model);

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserAccounts
                    .Where(x => x.Email == model.Email).FirstOrDefault();
                if (user != null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    //success
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim("Name", user.FirstName),
                        new Claim("Role", user.Role),
                        new Claim(ClaimTypes.Email, user.Email),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", "Username/Email lub hasło jest niepoprawne.");
                }

            }

            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Home");
        }
    }
}
