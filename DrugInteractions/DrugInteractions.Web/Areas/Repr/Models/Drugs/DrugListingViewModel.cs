using DrugInteractions.Services.Repr.Model;
using System.Collections.Generic;

namespace DrugInteractions.Web.Areas.Repr.Models.Drugs
{
    public class DrugListingViewModel
    {
        public IEnumerable<ReprDrugListingServiceModel> Drugs { get; set; }
    }
}
