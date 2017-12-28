using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Areas.Admin.Models.SideEffectGroups
{
    public class AddSideEffectGroupFormModel : IMapFrom<SideEffectGroup>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public User Admin { get; set; }

        public string AdminId { get; set; }

        public DateTime DateOfAddition { get; set; }
    }
}
