using FurniStyle.API.ErrorHandling.Middleware;
using FurniStyle.API;
using FurniStyle.API.ErrorHandling.Middleware;
using FurniStyle.Repository.Data.Context;
using FurniStyle.Repository.Data.DataSeeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FurniStyle.Repository.Identity.Context;
using FurniStyle.Repository.Identity.DataSeeding;
using FurniStyle.Core.Entities.Identity;

namespace EduTrack.API.Helpers
{
    public static class ConfigureMiddleware
    {
        public static async Task<WebApplication> ConfigureMiddlewaresAsync(this WebApplication app)
        {
            #region Migrate Migrations In Db While Runnig App And Seed Data As Well
                using var scope = app.Services.CreateScope();
                var serviceProvider = scope.ServiceProvider;

                var dataContext = serviceProvider.GetRequiredService<FurniStyleDbContext>();
                var identityContext = serviceProvider.GetRequiredService<FurniStyleIdentityDbContext>();
                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();


                try
                {
                    await dataContext.Database.MigrateAsync();
                    await FurniStyleDataSeeding.SeedingData(dataContext);

                    await identityContext.Database.MigrateAsync();

                    // Seed roles and users separately
                    //await FurniStyleIdentitySeedingRoles.SeedRoles(roleManager);
                    //await FurniStyleIdentitySeedingUsers.SeedingAdminAccount(userManager, roleManager);
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "There is an Error While Migration..!");
                }
            #endregion

            #region Swagger in Develpoment Mode
            app.UseCors("MyPolicy");

            app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Team4Store v1");
                    options.InjectStylesheet("/swagger/custom.css");
                    options.DocumentTitle = "Team4Store - Documentation";
                });
                if (true)
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

            #endregion

            #region Other Middlewares
                app.UseRouting();
                app.UseMiddleware<ExceptionMiddleware>();
                app.UseStatusCodePagesWithRedirects("/error/{0}");
                app.UseStaticFiles();
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

            #endregion

            return  app;
        }
    }
}
