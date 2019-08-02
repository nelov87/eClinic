namespace EClinic.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EClinic.Common;
    using EClinic.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public class SiteDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(EClinicDbContext dbContext, IServiceProvider serviceProvider)
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

            var userStore = serviceProvider.GetService<UserManager<EClinicUser>>();

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

			var AdminUserToAdd = new EClinicUser            {
                Email = "nelov87@gmail.com",
                FirstName = "Ivo",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov87",
                NormalizedEmail = "NELOV87@GMAIL.COM",
                NormalizedUserName = "NELOV87",
                Address = "Nqkyde Tam 35",
                Age = 25,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };

            var userToAdd = new EClinicUser
            {
                Email = "nelov872@gmail.com",
                FirstName = "Petyr",
                MiddleName = "Peshov",
                LastName = "Petrov",
                UserName = "nelov872",
                NormalizedEmail = "NELOV872@GMAIL.COM",
                NormalizedUserName = "NELOV872",
                Address = "I tuk i tam",
                Age = 30,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.UtcNow,
            };


            await userStore.CreateAsync(AdminUserToAdd, "123456");
            await userStore.CreateAsync(userToAdd, "123456");
            dbContext.SaveChanges();
            var adminUser = await userStore.FindByEmailAsync("nelov87@gmail.com");
            var user = await userStore.FindByEmailAsync("nelov872@gmail.com");
            await userStore.AddToRoleAsync(adminUser, GlobalConstants.AdministratorRoleName);
            await userStore.AddToRoleAsync(user, GlobalConstants.UserRoleName);
        }
    }
}