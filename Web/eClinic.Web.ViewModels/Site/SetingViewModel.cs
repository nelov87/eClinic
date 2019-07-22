using eClinic.Data.Models;
using eClinic.Services.Mapping;

namespace eClinic.Web.ViewModels.Site
{
    public class SetingViewModel : IMapFrom<Setting>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
