using DrugInteractions.Services.Repr.Model;
using System.Collections.Generic;

namespace DrugInteractions.Web.Areas.Repr.Models.SideEffects
{
    public class SideEffectListingViewModel
    {
        public IEnumerable<ReprSideEffectListingServiceModel> SideEffects { get; set; }
    }
}
