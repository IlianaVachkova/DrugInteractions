using DrugInteractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrugInteractions.Web.ViewComponents.Home
{
    public class WeeklyDrugsViewComponent : ViewComponent
    {
        private readonly IDrugService drugService;

        public WeeklyDrugsViewComponent(IDrugService drugService)
        {
            this.drugService = drugService;
        }

        [ResponseCache(Duration = 4 * 60 * 60)]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var drugsList = await this.drugService.GetWeeklyDrugs();

            return View(drugsList);
        }
    }
}
