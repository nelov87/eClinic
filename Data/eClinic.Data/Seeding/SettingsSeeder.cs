namespace EClinic.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EClinic.Data.Models;

    internal class SettingsSeeder : ISeeder
    {
        public async Task SeedAsync(EClinicDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Settings.Any())
            {
                return;
            }

            await dbContext.Settings.AddAsync(new Setting { Name = "Site Name", Value = "eClinic" });
            await dbContext.Settings.AddAsync(new Setting { Name = "Phone Number", Value = "+123 987 887 765" });
            await dbContext.Settings.AddAsync(new Setting { Name = "E-Mail", Value = "sales@smarteyeapps.com" });
        }
    }
}
