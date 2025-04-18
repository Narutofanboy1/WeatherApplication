﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public WeatherForecastsController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var weatherForecasts = await _context.WeatherForecasts.ToListAsync();
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

            var orderedWeatherForecasts = weatherForecasts
                .OrderBy(f => dayOrder[f.DayOfWeek]) 
                .ToList();

            return View(orderedWeatherForecasts);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DayOfWeek,WeatherDescription,MaxTemperature,MinTemperature," +
        "WindSpeed,WindDirection,RainProbability,Sunrise,Sunset,MoonPhase")] WeatherForecast weatherForecast)
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
                weatherForecast.DayDuration = weatherForecast.Sunset - weatherForecast.Sunrise;

                _context.Add(weatherForecast);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(weatherForecast);
        }


        [Authorize(Roles = "Admin")]
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

        

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DayOfWeek,WeatherDescription,MaxTemperature,MinTemperature," +
            "WindSpeed,WindDirection,RainProbability,Sunrise,Sunset,MoonPhase")] WeatherForecast weatherForecast)
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
                return RedirectToAction("Index", "Home");
            }
            return View(weatherForecast);
        }

        [Authorize(Roles = "Admin")]
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
            return RedirectToAction("Index", "Home");
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecasts.Any(e => e.Id == id);
        }
    }
}
