using AutoMapper;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Repr;
using DrugInteractions.Web.Areas.Repr.Models.SideEffects;
using DrugInteractions.Web.Infrastructure.Extensions;
using DrugInteractions.Web.Infrastructure.Populators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Areas.Repr.Controllers
{
    public class SideEffectsController : BaseReprController
    {
        private readonly IReprSideEffectsService reprSideEffectService;

        public SideEffectsController(IReprSideEffectsService reprSideEffectService, UserManager<User> userManager, IDropDownListPopulator populator)
            : base(userManager, populator)
        {
            this.reprSideEffectService = reprSideEffectService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceModel = await reprSideEffectService.AllAsync();

            var viewModel = new SideEffectListingViewModel { SideEffects = serviceModel };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var model = new SideEffectFormModel
            {
                SideEffectGroups = await this.populator.GetSideEffectGroups(),
                Drugs = await this.populator.GetDrugs()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SideEffectFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SideEffectGroups = await this.populator.GetSideEffectGroups();
                model.Drugs = await this.populator.GetDrugs();
                return View(model);
            }

            var dbModel = Mapper.Map<SideEffect>(model);

            var userId = this.userManager.GetUserId(User);
            dbModel.AdminId = userId;
            dbModel.DateOfAddition = DateTime.UtcNow;

            var successfulCreation = await this.reprSideEffectService.CreateAsync(dbModel);

            if (!successfulCreation)
            {
                ModelState.AddModelError(WebConstants.StatusMessage, WebConstants.SideEffectNameExists);
                model.SideEffectGroups = await this.populator.GetSideEffectGroups();
                model.Drugs = await this.populator.GetDrugs();
                return View(model);
            }

            if (model.DrugIds != null)
            {
                await this.reprSideEffectService.DrugsInSideEffect(model.DrugIds, dbModel.Id);
            }

            TempData.AddSuccessMessage($"Side effect {model.Name} successfully created.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int sideEffectId)
        {
            var dbModel = await this.reprSideEffectService.GetByIdAsync(sideEffectId);

            if (dbModel == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<SideEffectFormModel>(dbModel);

            viewModel.SideEffectGroups = await this.populator.GetSideEffectGroups();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SideEffectFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SideEffectGroups = await this.populator.GetSideEffectGroups();
                return View(model);
            }

            var dbModel = Mapper.Map<SideEffect>(model);

            var successfulEditing = await this.reprSideEffectService.UpdateAsync(dbModel);

            if (!successfulEditing)
            {
                ModelState.AddModelError(WebConstants.StatusMessage, WebConstants.SideEffectNameExists);
                model.SideEffectGroups = await this.populator.GetSideEffectGroups();
                return View(model);
            }

            TempData.AddSuccessMessage($"Side effect {model.Name} successfully updated.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int sideEffectId)
        {
            var dbModel = await this.reprSideEffectService.GetByIdAsync(sideEffectId);

            if (dbModel == null)
            {
                return NotFound();
            }

            await this.reprSideEffectService.DeleteAsync(dbModel);

            TempData.AddSuccessMessage($"Side effect {dbModel.Name} successfully deleted.");
            return RedirectToAction(nameof(Index));
        }
    }
}
