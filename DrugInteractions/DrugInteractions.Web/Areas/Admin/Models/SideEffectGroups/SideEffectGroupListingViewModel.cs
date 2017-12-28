using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;

namespace DrugInteractions.Web.Areas.Admin.Models.SideEffectGroups
{
    public class SideEffectGroupListingViewModel
    {
        public IEnumerable<AdminSideEffectGroupsListingServiceModel> SideEffectGroups { get; set; }
    }
}
