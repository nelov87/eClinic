namespace eClinic.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using eClinic.Common;
    using eClinic.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class SiteDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger(typeof(SiteDbContextSeeder));

            var userStore = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var seeders = new List<ISeeder>
                          {
                              new SiteSeeder(),
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext, serviceProvider);
                await dbContext.SaveChangesAsync();
                logger.LogInformation($"Seeder {seeder.GetType().Name} done.");
            }

            var userToAdd = new ApplicationUser
            {
                Email = "nelov87@gmail.com",
                FirstName = "Ivo",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov87@gmail.com",
                NormalizedEmail = "NELOV87@GMAIL.COM",
                NormalizedUserName = "NELOV87@GMAIL.COM",
                Address = "Nqkyde Tam 35",
                Age = 25,
                SecurityStamp = Guid.NewGuid().ToString(),
            };


            await userStore.CreateAsync(userToAdd, "123456");
            dbContext.SaveChanges();
            var user = await userStore.FindByEmailAsync("nelov87@gmail.com");
            await userStore.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}