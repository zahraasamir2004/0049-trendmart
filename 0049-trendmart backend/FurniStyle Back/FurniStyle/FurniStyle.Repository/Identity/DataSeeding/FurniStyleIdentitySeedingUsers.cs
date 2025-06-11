using FurniStyle.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Identity.DataSeeding
{
    public class FurniStyleIdentitySeedingUsers
    {
        public async static Task SeedingAdminAccount(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed the Admin user if no users exist
            if (userManager.Users.Count() == 0)
            {
                // Ensure the Admin role exists before adding the user to it
                var role = await roleManager.FindByNameAsync("Admin");
                if (role == null)
                {
                    role = new IdentityRole("Admin");
                    await roleManager.CreateAsync(role); // Create the role if it doesn't exist
                }

                var user = new ApplicationUser()
                {
                    Email = "admin@gmail.com",
                    PhoneNumber = "01098018628",
                    UserName = "Admin",
                    DisplayName = "Admin",
                    Address = new Address()
                    {
                        FName = "Admin",
                        LName = "Admin",
                        Street = "9hara",
                        City = "Helwan",
                        Country = "Egypt"
                    }
                };

                // Create the user with the specified password
                var result = await userManager.CreateAsync(user, "Admin1234@");

                if (result.Succeeded)
                {
                    // Add the user to the Admin role (role is already created)
                    await userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    // Optional: Handle error or log it if user creation fails
                }
            }
        }
    }
}
