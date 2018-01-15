using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Web.Areas.Admin.Models.SideEffectGroups
{
    public class SideEffectGroupFormModel : IMapFrom<SideEffectGroup>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(SideEffectGroupNameMinLength)]
        [MaxLength(SideEffectGroupNameMaxLength)]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        public string StatusMessage { get; set; }
    }
}
