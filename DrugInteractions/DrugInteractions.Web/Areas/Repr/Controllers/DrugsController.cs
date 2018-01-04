using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Repr;
using DrugInteractions.Web.Infrastructure.Populators;
using DrugInteractions.Web.Areas.Repr.Models.Drugs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Html;

namespace DrugInteractions.Web.Areas.Repr.Controllers
{
    public class DrugsController : BaseReprController
    {
        private readonly IReprDrugsService reprDrugsService;

        private readonly IHtmlService htmlService;

        public DrugsController(IReprDrugsService reprDrugsService, IHtmlService htmlService, UserManager<User> userManager, IDropDownListPopulator populator)
            : base(userManager, populator)
        {
            this.reprDrugsService = reprDrugsService;
            this.htmlService = htmlService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceModel = await reprDrugsService.AllAsync();

            var viewModel = new DrugListingViewModel { Drugs = serviceModel };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var model = new AddDrugFormModel
            {
                DrugGroups = await this.populator.GetDrugGroups(),
                Brands = await this.populator.GetBrands()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddDrugFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            model.Description = this.htmlService.Sanitize(model.Description);

            var dbModel = Mapper.Map<Drug>(model);

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Representative = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;

            await this.reprDrugsService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? drugId)
        {
            var dbModel = await this.reprDrugsService.GetByIdAsync(drugId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<AddDrugFormModel>(dbModel);

            viewModel.DrugGroups = await this.populator.GetDrugGroups();
            viewModel.Brands = await this.populator.GetBrands();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddDrugFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            model.Description = this.htmlService.Sanitize(model.Description);

            var dbModel = Mapper.Map<Drug>(model);

            await this.reprDrugsService.UpdateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? drugId)
        {
            var dbModel = await this.reprDrugsService.GetByIdAsync(drugId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            await this.reprDrugsService.DeleteAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
