using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EClinic.Data;
using EClinic.Data.Models;
using EClinic.Services.Mapping;
using EClinic.Web.InputModels;
using EClinic.Web.ViewModels.Site;

namespace EClinic.Services
{
    public class PageService : IPageService
    {
        private readonly EClinicDbContext db;

        public PageService(EClinicDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> EditPage(PageInputModel pageInput)
        {
            var page = this.db.SitePages.FirstOrDefault(x => x.Id == pageInput.Id);
            page.Title = pageInput.Title;
            page.Content = pageInput.Content;
            page.ImageUrl = pageInput.ImageUrl;
            page.ModifiedOn = DateTime.UtcNow;

            int result = await this.db.SaveChangesAsync();

            if (!(result == 0))
            {
                return false;
            }
            return true;
        }

        public async Task<ICollection<PageViewModel>> GetAllPages()
        {
            var pages = this.db.SitePages.To<PageViewModel>().ToList();

            return pages;
        }

        public async Task<PageViewModel> GetPage(string id)
        {
            return this.db.SitePages.To<PageViewModel>().FirstOrDefault(x => x.Id == id);
        }

        public async Task<bool> AddPage(NewPageInputModel pageInput)
        {
            var page = new SitePages();
            page.Title = pageInput.Title;
            page.Content = pageInput.Content;
            page.ImageUrl = pageInput.ImageUrl;
            page.CreatedOn = DateTime.UtcNow;

            try
            {
                await this.db.SitePages.AddAsync(page);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("Add Page Needs a valid page model.");
            }

            int result = await this.db.SaveChangesAsync();

            if (!(result == 0))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeletePage(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("Delete page requires valid id.");
            }


            var page = this.db.SitePages.FirstOrDefault(x => x.Id == id);

            //TODO try catch ??? 
            this.db.SitePages.Remove(page);

            int result = await this.db.SaveChangesAsync();

            if (!(result == 0))
            {
                return false;
            }
            return true;
        }
    }
}
