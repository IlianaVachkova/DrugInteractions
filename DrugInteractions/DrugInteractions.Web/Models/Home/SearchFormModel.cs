using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Models.Home
{
    public class SearchFormModel
    {
        public string SearchText { get; set; }

        [Display(Name ="Search drug by name")]
        public bool SearchDrugByName { get; set; }

        [Display(Name = "Search drug by brand")]
        public bool SearchDrugByBrand { get; set; }

        [Display(Name = "Search drug by drug group")]
        public bool SearchDrugByDrugGroup { get; set; }

        [Display(Name = "Search drug by medical representative")]
        public bool SearchDrugByRepresentative { get; set; }
    }
}
