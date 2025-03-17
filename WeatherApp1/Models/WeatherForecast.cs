using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherApp1.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }

        [Display(Name = "Ден от седмицата")]
        [Required(ErrorMessage = "Въведете ден от седмицата.")]
        public string DayOfWeek { get; set; }

        [Display(Name = "Описание на времето")]
        [Required(ErrorMessage = "Въведете описание на времето.")]
        public string WeatherDescription { get; set; }

        [Display(Name = "Максимална температура")]
        [Required(ErrorMessage = "Въведете максимална температура.")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Максималната температура трябва да е валидно число.")]
        public int MaxTemperature { get; set; }

        [Display(Name = "Минимална температура")]
        [Required(ErrorMessage = "Въведете минимална температура.")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "Минималната температура трябва да е валидно число.")]
        public int MinTemperature { get; set; }

        [Display(Name = "Скорост на вятъра")]
        [Required(ErrorMessage = "Въведете скорост на вятъра.")]
        [Range(0, int.MaxValue, ErrorMessage = "Скоростта на вятъра не може да бъде отрицателна.")]
        public int WindSpeed { get; set; }

        [Display(Name = "Посока на вятъра")]
        [Required(ErrorMessage = "Въведете посока на вятъра.")]
        public string WindDirection { get; set; }

        [Display(Name = "Вероятност за дъжд")]
        [Required(ErrorMessage = "Въведете вероятност за дъжд.")]
        [Range(0, 100, ErrorMessage = "Вероятността за дъжд трябва да е между 0 и 100 %.")]
        public int RainProbability { get; set; }

        [Display(Name = "Час на изгрева")]
        [Required(ErrorMessage = "Въведете час на изгрева.")]
        public TimeSpan Sunrise { get; set; }

        [Display(Name = "Час на залеза")]
        [Required(ErrorMessage = "Въведете час на залеза.")]
        public TimeSpan Sunset { get; set; }

        [Display(Name = "Фаза на луната")]
        [Required(ErrorMessage = "Въведете фазата на луната.")]
        public string MoonPhase { get; set; }
    }
}
