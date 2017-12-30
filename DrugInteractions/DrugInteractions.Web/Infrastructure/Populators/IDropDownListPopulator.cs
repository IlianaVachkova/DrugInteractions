using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Infrastructure.Populators
{
    public interface IDropDownListPopulator
    {
        Task<IEnumerable<SelectListItem>> GetSideEffectGroups();

        Task<IEnumerable<SelectListItem>> GetDrugGroups();

        Task<IEnumerable<SelectListItem>> GetBrands();
    }
}
