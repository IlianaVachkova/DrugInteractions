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
using DrugInteractions.Web.Infrastructure.Extensions;

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
            var model = new DrugFormModel
            {
                DrugGroups = await this.populator.GetDrugGroups(),
                Brands = await this.populator.GetBrands()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DrugFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            model.Description = this.htmlService.Sanitize(model.Description);

            var dbModel = Mapper.Map<Drug>(model);

            var userId = this.userManager.GetUserId(User);
            dbModel.RepresentativeId = userId;
            dbModel.DateOfAddition = DateTime.UtcNow;

            try
            {
                await this.reprDrugsService.CreateAsync(dbModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Drug with this name already exists.");
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            TempData.AddSuccessMessage($"Drug {model.Name} successfully created.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int drugId)
        {
            var dbModel = await this.reprDrugsService.GetByIdAsync(drugId);

            if (dbModel == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<DrugFormModel>(dbModel);

            viewModel.DrugGroups = await this.populator.GetDrugGroups();
            viewModel.Brands = await this.populator.GetBrands();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DrugFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            model.Description = this.htmlService.Sanitize(model.Description);

            var dbModel = Mapper.Map<Drug>(model);

            try
            {
                await this.reprDrugsService.UpdateAsync(dbModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Drug with this name already exists.");
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            TempData.AddSuccessMessage($"Drug {model.Name} successfully updated.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int drugId)
        {
            var dbModel = await this.reprDrugsService.GetByIdAsync(drugId);

            if (dbModel == null)
            {
                return NotFound();
            }

            await this.reprDrugsService.DeleteAsync(dbModel);

            TempData.AddSuccessMessage($"Drug {dbModel.Name} successfully deleted.");
            return RedirectToAction(nameof(Index));
        }
    }
}
