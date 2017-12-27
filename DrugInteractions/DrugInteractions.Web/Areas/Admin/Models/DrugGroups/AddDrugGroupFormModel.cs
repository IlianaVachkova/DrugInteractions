using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Drugs;
using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Areas.Admin.Models.DrugGroups
{
    public class AddDrugGroupFormModel : IMapFrom<DrugGroup>
    {
        [Required]
        public string Name { get; set; }
    }
}
