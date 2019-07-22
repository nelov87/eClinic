namespace eClinic.Data.Models
{
    using eClinic.Data.Common.Models;

    public class Setting : BaseDeletableModel<int>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
