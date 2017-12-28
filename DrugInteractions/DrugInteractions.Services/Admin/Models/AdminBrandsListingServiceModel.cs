using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;

namespace DrugInteractions.Services.Admin.Models
{
    public class AdminBrandsListingServiceModel : IMapFrom<Brand>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string WebSite { get; set; }
    }
}
