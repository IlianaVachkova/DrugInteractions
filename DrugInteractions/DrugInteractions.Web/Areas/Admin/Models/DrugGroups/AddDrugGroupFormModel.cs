using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Web.Areas.Admin.Models.DrugGroups
{
    public class AddDrugGroupFormModel : IMapFrom<DrugGroup>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(DrugGroupNameMinLength)]
        [MaxLength(DrugGroupNameMaxLength)]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }
    }
}
