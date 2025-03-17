using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WeatherApp1.Data;
using WeatherApp1.Models;

namespace WeatherApp1.Controllers
{
    public class WeatherForecastsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WeatherForecastsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WeatherForecasts
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

            // Order the weather forecasts by the custom day order
            var orderedWeatherForecasts = weatherForecasts
                .OrderBy(f => dayOrder[f.DayOfWeek]) // Sort using the dictionary for the correct order
                .ToList();

            // Return the view with the ordered list
            return View(orderedWeatherForecasts);
        }


        // GET: WeatherForecasts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherForecast = await _context.WeatherForecasts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WeatherForecasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,WeatherDescription,MaxTemperature,MinTemperature,WindSpeed,WindDirection,RainProbability,Sunrise,Sunset,MoonPhase")] WeatherForecast weatherForecast/*, List<string> WeatherDescriptions*/)
        {
            if (weatherForecast.MaxTemperature < weatherForecast.MinTemperature)
            {
                ModelState.AddModelError("MaxTemperature", "Максималната температура трябва да бъде по-голяма от минималната температура.");
                ModelState.AddModelError("MinTemperature", "Минималната температура трябва да бъде по-малка от максималната температура.");
            }

            if (weatherForecast.WindSpeed < 0)
            {
                ModelState.AddModelError("WindSpeed", "Скоростта на вятъра не може да бъде отрицателна.");
            }

            if (weatherForecast.RainProbability < 0 || weatherForecast.RainProbability > 100)
            {
                ModelState.AddModelError("RainProbability", "Вероятността за дъжд трябва да е между 0 и 100 %.");
            }

            if (weatherForecast.Sunrise >= weatherForecast.Sunset)
            {
                ModelState.AddModelError("Sunrise", "Изгревът трябва да е преди залеза.");
                ModelState.AddModelError("Sunset", "Залезът трябва да е след изгрева.");

            }

            if (ModelState.IsValid)
            {
                _context.Add(weatherForecast);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }
            return View(weatherForecast);
        }

        // POST: WeatherForecasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayOfWeek,WeatherDescription,MaxTemperature,MinTemperature,WindSpeed,WindDirection,RainProbability,Sunrise,Sunset,MoonPhase")] WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return NotFound();
            }

            if (weatherForecast.MaxTemperature < weatherForecast.MinTemperature)
            {
                ModelState.AddModelError("MaxTemperature", "Максималната температура трябва да бъде по-голяма от минималната температура.");
                ModelState.AddModelError("MinTemperature", "Минималната температура трябва да бъде по-малка от максималната температура.");
            }

            if (weatherForecast.WindSpeed < 0)
            {
                ModelState.AddModelError("WindSpeed", "Скоростта на вятъра не може да бъде отрицателна.");
            }

            if (weatherForecast.RainProbability < 0 || weatherForecast.RainProbability > 100)
            {
                ModelState.AddModelError("RainProbability", "Вероятността за дъжд трябва да е между 0 и 100 %.");
            }

            if (weatherForecast.Sunrise >= weatherForecast.Sunset)
            {
                ModelState.AddModelError("Sunrise", "Изгревът трябва да е преди залеза.");
                ModelState.AddModelError("Sunset", "Залезът трябва да е след изгрева.");
            }

        //    if (!string.IsNullOrEmpty(weatherForecast.MoonPhase))
          //  {
           //     string moonPhaseImagePath = $"/images/moon_phases/{weatherForecast.MoonPhase}.jpg";
                // You can check if the file actually exists, but since the file paths should already be defined, we'll skip this step
           // }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(weatherForecast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeatherForecastExists(weatherForecast.Id))
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
            return View(weatherForecast);
        }

        // GET: WeatherForecasts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var weatherForecast = await _context.WeatherForecasts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            return View(weatherForecast);
        }

        // POST: WeatherForecasts/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var weatherForecast = await _context.WeatherForecasts.FindAsync(id);
            if (weatherForecast != null)
            {
                _context.WeatherForecasts.Remove(weatherForecast);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecasts.Any(e => e.Id == id);
        }
    }
}
