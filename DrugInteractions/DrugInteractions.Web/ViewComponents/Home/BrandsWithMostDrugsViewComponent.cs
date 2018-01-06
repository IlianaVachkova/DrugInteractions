using DrugInteractions.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrugInteractions.Web.ViewComponents.Home
{
    public class BrandsWithMostDrugsViewComponent : ViewComponent
    {
        private readonly IBrandService brandService;

        public BrandsWithMostDrugsViewComponent(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brandsList =await this.brandService.GetBrandsWithMostDrugs(WebConstants.ChartCountOfBrands);

            return View(brandsList);
        }
    }
}
