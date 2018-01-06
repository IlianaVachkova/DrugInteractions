using DrugInteractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrugInteractions.Web.ViewComponents.Home
{
    public class DrugsWithLessSideEffectsViewComponent : ViewComponent
    {
        private readonly IDrugService drugService;

        public DrugsWithLessSideEffectsViewComponent(IDrugService drugService)
        {
            this.drugService = drugService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var drugsList = await this.drugService.GetDrugsWithLessSideEffects(WebConstants.ChartCountOfDrugs);

            return View(drugsList);
        }
    }
}
