using DrugInteractions.Data;
using DrugInteractions.Data.Models.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<DrugInteractionsDbContext>().Database.Migrate();

          //  var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
          //  var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
          //  
          //  Task
          //   .Run(async () =>
          //   {
          //       var adminName = WebConstants.AdministratorRole;
          //  
          //       var roles = new[]
          //       {
          //        adminName,
          //        WebConstants.RepresentativeRole
          //       };
          //  
          //       foreach (var role in roles)
          //       {
          //           var roleExists = await roleManager.RoleExistsAsync(role);
          //  
          //           if (!roleExists)
          //           {
          //               await roleManager.CreateAsync(new IdentityRole { Name = role });
          //           }
          //       }
          //  
          //       var representativeEmail = "repr@drugs.com";
          //  
          //       var representaiveUser = await userManager.FindByEmailAsync(representativeEmail);
          //  
          //       if (representaiveUser==null)
          //       {
          //           representaiveUser = new User
          //           {
          //               Email = representativeEmail,
          //               UserName = WebConstants.RepresentativeRole,
          //               Name = WebConstants.RepresentativeRole,
          //               BirthDate = DateTime.UtcNow,
          //               LinkedIn = "linkedin.com",
          //               Facebook = "facebook.com",
          //               DateOfAddition = DateTime.UtcNow,
          //           };
          //       }
          //  
          //       await userManager.CreateAsync(representaiveUser, "reprDrug");
          //  
          //       await userManager.AddToRoleAsync(representaiveUser, WebConstants.RepresentativeRole);
          //  
          //      // var adminEmail = "admin@drugs.com";
          //      //
          //      // var adminUser = await userManager.FindByEmailAsync(adminEmail);
          //      //
          //      // if (adminUser == null)
          //      // {
          //      //     adminUser = new User
          //      //     {
          //      //         Email = adminEmail,
          //      //         UserName = adminName,
          //      //         Name = adminName,
          //      //         BirthDate = DateTime.UtcNow,
          //      //         LinkedIn = "linkedin.com",
          //      //         Facebook = "facebook.com",
          //      //         DateOfAddition = DateTime.UtcNow,
          //      //     };
          //      // }
          //      //
          //      // await userManager.CreateAsync(adminUser, "adminDrug");
          //      //
          //      // await userManager.AddToRoleAsync(adminUser, adminName);
          //   })
          //      .Wait();
            }

            return app;
        }
    }
}