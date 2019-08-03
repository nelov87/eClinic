namespace EClinic.Data.Seeding
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(EClinicDbContext dbContext, IServiceProvider serviceProvider);
    }
}
