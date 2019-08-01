namespace EClinic.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using EClinic.Data.Models;
    using Microsoft.AspNetCore.Identity;

    internal class SiteSeeder : ISeeder
    {
        public async Task SeedAsync(EClinicDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.SitePages.Any())
            {
                return;
            }

            await dbContext.SitePages.AddAsync(new SitePages { Title = "About Us", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer neque libero, pulvinar et elementum quis, facilisis eu ante. " +
                "Mauris non placerat sapien. Pellentesque tempor arcu non odio scelerisque ullamcorper. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam varius eros consequat auctor gravida. Fusce tristique lacus" +
                " at urna sollicitudin pulvinar. Suspendisse hendrerit ultrices mauris." +
                "Ut ultricies lacus a rutrum mollis.Orci varius natoque penatibus et magnis dis parturient montes, " +
                "nascetur ridiculus mus.Sed porta dolor quis felis pulvinar dignissim.Etiam nisl ligula, " +
                "ullamcorper non metus vitae, " +
                "maximus efficitur mi.Vivamus ut ex ullamcorper," +
                " scelerisque lacus nec, " +
                "commodo dui.Proin massa urna, volutpat vel augue eget, iaculis tristique dui.",
            });

            
            //await dbContext.Users.AddAsync(
            //});


        }

    }
}

