using System.ComponentModel.DataAnnotations;

namespace WeatherApp1.Enums
{
    public enum Roles
    {
        [Display(Name = "Администратор")]
        Admin,

        [Display(Name = "Регистриран")]
        Registered,

        [Display(Name = "Гост")]
        Guest 
    }
}
