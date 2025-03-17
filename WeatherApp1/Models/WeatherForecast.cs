using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp1.Models;

public class WeatherForecast
{
    public int Id { get; set; }
    public string DayOfWeek { get; set; }
    public string WeatherDescription { get; set; } // "predimno slanchevo", etc...
    public int MaxTemperature { get; set; }
    public int MinTemperature { get; set; }
    public int WindSpeed { get; set; }
    public string WindDirection { get; set; }
    public int RainProbability { get; set; }
    public TimeSpan Sunrise { get; set; }
    public TimeSpan Sunset { get; set; }
    public string MoonPhase { get; set; }
}
