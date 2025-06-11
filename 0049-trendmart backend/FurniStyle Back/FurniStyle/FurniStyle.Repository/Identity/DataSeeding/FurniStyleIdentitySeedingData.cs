using FurniStyle.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Identity.DataSeeding
{
    public static class FurniStyleIdentitySeedingData
    {
        public async static Task SeedingData(UserManager<ApplicationUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                var user = new ApplicationUser()
                {
                    Email = "hussein@gmail.com",
                    PhoneNumber = "01098018628",
                    UserName = "hussein",
                    DisplayName = "Hussein Adel",
                    Address = new Address()
                    {
                        FName = "Hussein",
                        LName = "Adel",
                        Street = "9hara",
                        City = "Helwan",
                        Country = "Egypt"
                    }
                };
                await userManager.CreateAsync(user, "Huss1234@");
            }

        }

    }
}
