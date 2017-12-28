using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;

namespace DrugInteractions.Web.Areas.Admin.Models.Brands
{
    public class BrandListingViewModel
    {
        public IEnumerable<AdminBrandsListingServiceModel> Brands { get; set; }
    }
}
