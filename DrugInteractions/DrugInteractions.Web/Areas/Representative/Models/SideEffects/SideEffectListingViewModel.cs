using DrugInteractions.Services.Representative.Model;
using System.Collections.Generic;

namespace DrugInteractions.Web.Areas.Representative.Models.SideEffects
{
    public class SideEffectListingViewModel
    {
        public IEnumerable<RepresentativeSideEffectListingServiceModel> SideEffects { get; set; }
    }
}
