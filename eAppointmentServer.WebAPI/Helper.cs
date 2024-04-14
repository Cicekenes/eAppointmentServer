using eAppointmentServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace eAppointmentServer.WebAPI
{
    public static class Helper
    {
        public static async Task CreateUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                if (!userManager.Users.Any())
                {
                    await userManager.CreateAsync(new AppUser()
                    {
                        FirstName = "Enes",
                        LastName = "Çiçek",
                        Email = "admin@admin.com",
                        UserName = "admin"
                    }, "1");
                }
            }
        }
    }
}
