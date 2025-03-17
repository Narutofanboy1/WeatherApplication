using Microsoft.AspNetCore.Identity;
using WeatherApp1.Enums;

namespace WeatherApp1.Data
{
    public class ContextSeed
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (string roleName in Enum.GetNames(typeof(Roles)))
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new()
            {
                UserName = "Admin",
                Email = "svetlosino123@gmail.com",
                FirstName = "Svetlozar",
                LastName = "Trifonov",
                EmailConfirmed = true
                
            };

            ApplicationUser foundUser = await userManager.FindByEmailAsync(defaultUser.Email);

            if (foundUser == null)
            {
                await userManager.CreateAsync(defaultUser, "123456789Zarko!");

                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            }
        }
    }
}
