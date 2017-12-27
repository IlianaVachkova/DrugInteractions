using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Drugs;

namespace DrugInteractions.Services.Admin.Models
{
    public class AdminDrugGroupListingServiceModel : IMapFrom<DrugGroup>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
