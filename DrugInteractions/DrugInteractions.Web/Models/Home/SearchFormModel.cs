using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Models.Home
{
    public class SearchFormModel
    {
        public string SearchText { get; set; }

        public SearchDrugByType SearchDrugBy { get; set; }
    }
}
