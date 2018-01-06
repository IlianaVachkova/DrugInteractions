using DrugInteractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrugInteractions.Web.ViewComponents.Home
{
    [Authorize]
    public class WeeklyDrugsViewComponent : ViewComponent
    {
        private readonly IDrugService drugService;

        public WeeklyDrugsViewComponent(IDrugService drugService)
        {
            this.drugService = drugService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var drugsList = await this.drugService.GetWeeklyDrugs();

            return View(drugsList);
        }
    }
}
