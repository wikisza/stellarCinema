﻿using Microsoft.AspNetCore.Mvc;

namespace stellarCinema.Controllers
{
    public class ReservationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
