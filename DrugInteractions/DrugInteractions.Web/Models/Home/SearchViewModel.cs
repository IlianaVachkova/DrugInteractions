using DrugInteractions.Services.Models;
using System.Collections.Generic;

namespace DrugInteractions.Web.Models.Home
{
    public class SearchViewModel
    {
        public IEnumerable<DrugListingServiceModel> Drugs { get; set; }
            = new List<DrugListingServiceModel>();

        public string SearchText { get; set; }
    }
}
