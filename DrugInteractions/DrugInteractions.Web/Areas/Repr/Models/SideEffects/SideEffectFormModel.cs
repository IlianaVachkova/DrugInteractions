using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static DrugInteractions.Data.Models.DataConstants;

namespace DrugInteractions.Web.Areas.Repr.Models.SideEffects
{
    public class SideEffectFormModel : IMapFrom<SideEffect>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(SideEffectNameMinLength)]
        [MaxLength(SideEffectNameMaxLength)]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }

        [Display(Name="Side Effect Group")]
        public int SideEffectGroupId { get; set; }

        public SideEffectGroup SideEffectGroup { get; set; }

        public IEnumerable<SelectListItem> SideEffectGroups { get; set; }
    }
}
