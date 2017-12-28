using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Areas.Admin.Models.Brands
{
    public class AddBrandFormModel : IMapFrom<Brand>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string WebSite { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }
    }
}
