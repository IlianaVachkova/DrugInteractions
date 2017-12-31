using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;

namespace DrugInteractions.Services.Models
{
    public class DrugListingServiceModel : IMapFrom<Drug>
    {
        public string Name { get; set; }

        public DrugGroup DrugGroup { get; set; }

        public Brand Brand { get; set; }

        public User Representative { get; set; }
    }
}
