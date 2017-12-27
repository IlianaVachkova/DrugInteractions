using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Areas.Admin.Models.DrugGroups
{
    public class AddDrugGroupFormModel : IMapFrom<DrugGroup>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }
    }
}
