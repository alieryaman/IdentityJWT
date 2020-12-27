using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Data
{
    public class AppIdentitiyDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppIdentitiyDbContext(DbContextOptions<AppIdentitiyDbContext> options) : base(options)
        {




        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


    }
}
