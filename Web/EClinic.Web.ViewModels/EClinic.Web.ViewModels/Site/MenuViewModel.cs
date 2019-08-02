using EClinic.Data.Models;
using EClinic.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace EClinic.Web.ViewModels.Site
{
    public class MenuViewModel : IMapFrom<SitePages>
    {
        public string Id { get; set; }

        public string Title { get; set; }
    }
}
