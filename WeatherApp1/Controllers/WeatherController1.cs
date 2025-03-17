//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using WeatherApp1.Data;
//using WeatherApp1.Models;
//using System.Linq;

//namespace WeatherApp.Controllers
//{
//    [Authorize(Roles = "Admin")]
//    public class WeatherController1 : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public WeatherController1(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public IActionResult Index()
//        {
//            var forecasts = _context.WeatherForecasts.ToList();
//            return View(forecasts);
//        }

//        public IActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public IActionResult Create(WeatherForecast forecast)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.WeatherForecasts.Add(forecast);
//                _context.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(forecast);
//        }

//        public IActionResult Edit(int id)
//        {
//            var forecast = _context.WeatherForecasts.Find(id);
//            if (forecast == null)
//            {
//                return NotFound();
//            }
//            return View(forecast);
//        }

//        [HttpPost]
//        public IActionResult Edit(WeatherForecast forecast)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.WeatherForecasts.Update(forecast);
//                _context.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(forecast);
//        }

//        public IActionResult Delete(int id)
//        {
//            var forecast = _context.WeatherForecasts.Find(id);
//            if (forecast == null)
//            {
//                return NotFound();
//            }
//            return View(forecast);
//        }

//        [HttpPost, ActionName("Delete")]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var forecast = _context.WeatherForecasts.Find(id);
//            if (forecast != null)
//            {
//                _context.WeatherForecasts.Remove(forecast);
//                _context.SaveChanges();
//            }
//            return RedirectToAction("Index");
//        }
//    }
//}
