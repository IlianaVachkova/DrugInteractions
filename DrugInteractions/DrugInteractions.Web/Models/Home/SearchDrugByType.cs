using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Models.Home
{
    public enum SearchDrugByType
    {
        [Display(Name = "Search drug by name")]
        SearchDrugByName = 0,
        [Display(Name = "Search drug by brand")]
        SearchDrugByBrand = 1,
        [Display(Name = "Search drug by drug group")]
        SearchDrugByDrugGroup = 2,
        [Display(Name = "Search drug by medical representative")]
        SearchDrugByRepresentative = 3
    }
}
