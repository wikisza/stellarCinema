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
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var users = await _context.UserAccounts
            .ToListAsync();

            return View(users);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.UserAccounts
                .FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, FirstName, LastName, Email, PhoneNumber")] UserAccount account)
        {

            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.UserAccounts.FindAsync(id);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }
                    existingUser.FirstName = account.FirstName;
                    existingUser.LastName = account.LastName;
                    existingUser.Email = account.Email;
                    existingUser.PhoneNumber = account.PhoneNumber;


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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

        private bool AccountExists(int Id)
        {
            return _context.UserAccounts.Any(e => e.Id == Id);
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.UserAccounts
                .FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _context.UserAccounts
                .FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.UserAccounts.FindAsync(id);
            if (account != null)
            {
                _context.UserAccounts.Remove(account);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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

        public IActionResult Employee_Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Employee_Registration(RegistrationViewModel model)
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


                UserAccount account = new UserAccount
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = hashedPassword,
                    PhoneNumber = model.PhoneNumber,
                    Role = model.Role,
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
                    .Where(x => x.Email == model.Email)
                    .FirstOrDefault();

                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    ModelState.AddModelError("", "Email lub hasło jest niepoprawne.");
                    return View(model);
                }

                // Logowanie powiodło się
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Email, user.Email),
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Home");
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
