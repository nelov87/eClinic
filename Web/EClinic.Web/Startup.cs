using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Common;
using EClinic.Services;
using EClinic.Services.Mapping;
using System.Reflection;
using EClinic.Web.ViewModels.Site;
using EClinic.Data.Seeding;
using EClinic.Services.FrontEnd;
using EClinic.Services.Administration;
using EClinic.Services.Exams;

namespace EClinic.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EClinicDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<EClinicUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<EClinicDbContext>();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/Account/Login");



            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = true;
                option.Password.RequiredLength = 3;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredUniqueChars = 0;

                option.User.RequireUniqueEmail = true;
            });

            //User Services registration
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<ISiteService, SiteService>();
            services.AddTransient<IMenuService, MenuService>();
            services.AddTransient<IPageService, PageService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IExamService, ExamService>();


            services.AddMvc(options => 
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute())
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(SetingViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<EClinicDbContext>();
                
                dbContext.Database.EnsureCreated();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                new SiteDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseDatabaseErrorPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
