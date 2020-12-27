using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityJWT.Data
{
    public  class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppIdentitiyDbContext>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "besiraydemir",
                    Email = "postauyelik@gmailc.om",
                    SecurityStamp=Guid.NewGuid().ToString()
                };

                UserManager.CreateAsync(user,"Kdk2011.");

            }

        }

   


    }
}
