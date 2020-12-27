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

        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppIdentitiyDbContext>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager= serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string rolAdmin= "admin";
            string rolEditor = "editor";
            context.Database.EnsureCreated();

            if(!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "besiraydemir",
                    Email = "postauyelik@gmailc.om",
                    SecurityStamp=Guid.NewGuid().ToString()
                };

              await  UserManager.CreateAsync(user,"Kdk2011.");

          
            if (!context.Roles.Any())
            {
                //roleManager.FindByNameAsync(rolAdmin);
               // roleManager.FindByNameAsync(rolEditor);

                if (await roleManager.FindByNameAsync(rolAdmin)==null)
                {
                        await roleManager.CreateAsync(new IdentityRole(rolAdmin));
                }

                if (await roleManager.FindByNameAsync(rolEditor) == null)
                {
                        await roleManager.CreateAsync(new IdentityRole(rolEditor));
                }
            }

                await UserManager.AddToRoleAsync(user, rolAdmin);

            }




        }

   


    }
}
