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
            var weatherForecasts = await _context.WeatherForecasts.ToListAsync();
            var dayOrder = new Dictionary<string, int>
            {
                { "Понеделник", 1 },
                { "Вторник", 2 },
                { "Сряда", 3 },
                { "Четвъртък", 4 },
                { "Петък", 5 },
                { "Събота", 6 },
                { "Неделя", 7 }
            };

          var orderedWeatherForecasts = weatherForecasts
                .OrderBy(f => dayOrder[f.DayOfWeek]) 
                .ToList();
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
