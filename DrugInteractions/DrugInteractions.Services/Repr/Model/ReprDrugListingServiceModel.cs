using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Drugs;

namespace DrugInteractions.Services.Repr.Model
{
    public class ReprDrugListingServiceModel : IMapFrom<Drug>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ActiveSubstances { get; set; }

        public string Excipients { get; set; }

        public PharmaceuticalFormType PharmaceuticalForm { get; set; }

        public string Usage { get; set; }

        public string Description { get; set; }

        public DrugGroup DrugGroup { get; set; }

        public Brand Brand { get; set; }
    }
}
