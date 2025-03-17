using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeatherApp1.Data;

namespace WeatherApp1
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<ApplicationUser>().AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            using (IServiceScope service = app.Services.CreateScope())
            {
                IServiceProvider provider = service.ServiceProvider;

                RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                await ContextSeed.SeedRoleAsync(roleManager);

                UserManager<ApplicationUser> userManager = provider.GetRequiredService<UserManager<ApplicationUser>>();
                await ContextSeed.SeedAdminAsync(userManager);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
