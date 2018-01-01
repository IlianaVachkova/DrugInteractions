using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DrugInteractions.Web.Models;
using DrugInteractions.Web.Models.Home;
using DrugInteractions.Services;
using Microsoft.AspNetCore.Authorization;

namespace DrugInteractions.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDrugService drugService;

        public HomeController(IDrugService drugService)
        {
            this.drugService = drugService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Search(SearchFormModel model)
        {
            var viewModel = new SearchViewModel
            {
                SearchText = model.SearchText,
            };

            switch (model.SearchDrugBy)
            {
                case SearchDrugByType.SearchDrugByName:
                    viewModel.Drugs = await this.drugService.FindByNameAsync(model.SearchText);
                    break;
                case SearchDrugByType.SearchDrugByBrand:
                    viewModel.Drugs = await this.drugService.FindByBrandAsync(model.SearchText);
                    break;
                case SearchDrugByType.SearchDrugByDrugGroup:
                    viewModel.Drugs = await this.drugService.FindByDrugGroupAsync(model.SearchText);
                    break;
                case SearchDrugByType.SearchDrugByRepresentative:
                    viewModel.Drugs = await this.drugService.FindByRepresentativeAsync(model.SearchText);
                    break;
                default: break;
            }

            return View(viewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
