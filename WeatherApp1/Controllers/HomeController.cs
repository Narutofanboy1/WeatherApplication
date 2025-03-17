using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WeatherApp1.Data;
using WeatherApp1.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace WeatherApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Fetch all weather forecasts from the database
            var weatherForecasts = await _context.WeatherForecasts.ToListAsync();

            // Create a dictionary to map days of the week to a numerical value
            var dayOrder = new Dictionary<string, int>
            {
                { "Monday", 1 },
                { "Tuesday", 2 },
                { "Wednesday", 3 },
                { "Thursday", 4 },
                { "Friday", 5 },
                { "Saturday", 6 },
                { "Sunday", 7 }
            };

            // Map each moon phase to its corresponding image
           // var moonImages = new Dictionary<string, string>
          //  {
             //   { "New Moon", "NewMoon.png" },
            //    { "Waxing Crescent", "WaxingCrescent.png" },
             //   { "First Quarter", "FirstQuarter.png" },
              //  { "Waxing Gibbous", "WaxingGibbous.png" },
               // { "Full Moon", "FullMoon.png" },
             //   { "Waning Gibbous", "WaningGibbous.png" },
             //   { "Last Quarter", "LastQuarter.png" },
              //  { "Waning Crescent", "WaningCrescent.png" }
          //  };

            // Order the weather forecasts by the custom day order
            var orderedWeatherForecasts = weatherForecasts
                .OrderBy(f => dayOrder[f.DayOfWeek]) // Sort using the dictionary for the correct order
                .ToList();

            // Assign the correct image path to each weather forecast's MoonPhase
          //  foreach (var forecast in orderedWeatherForecasts)
           // {
           //     if (moonImages.ContainsKey(forecast.MoonPhase))
             //   {
              //      forecast.MoonPhase = $"/images/{moonImages[forecast.MoonPhase]}";
              ///  }
               // else
               // {
                    // Optionally, you can provide a default image or handle unknown phases
               //     forecast.MoonPhase = "/images/DefaultMoon.png"; // Make sure you have a default image if needed
               // }
           // }

            return View(orderedWeatherForecasts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
