using DrugInteractions.Common.Mapping;
using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;

namespace DrugInteractions.Web.Areas.Admin.Models.DrugGroups
{
    public class DrugGroupListingViewModel
    {
        public IEnumerable<AdminDrugGroupListingServiceModel> DrugGroups { get; set; }
    }
}
