using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp1.Data
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Име")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

    }
}
