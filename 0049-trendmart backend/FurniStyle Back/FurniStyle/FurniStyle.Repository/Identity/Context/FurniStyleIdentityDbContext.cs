using FurniStyle.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurniStyle.Repository.Identity.Context
{
    public class FurniStyleIdentityDbContext:IdentityDbContext<ApplicationUser>
    {
        public FurniStyleIdentityDbContext(DbContextOptions<FurniStyleIdentityDbContext> options) : base(options)
        {

        }
    }
}
