using AutoMapper;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.SideEffectGroups;
using DrugInteractions.Web.Infrastructure.Extensions;
using DrugInteractions.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Areas.Admin.Controllers
{
    public class SideEffectGroupsController : BaseAdminController
    {
        private readonly IAdminSideEffectGroupsService adminSideEffectGroupsService;

        public SideEffectGroupsController(IAdminSideEffectGroupsService adminSideEffectGroupsService, UserManager<User> userManager)
            : base(userManager)
        {
            this.adminSideEffectGroupsService = adminSideEffectGroupsService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceModel = await adminSideEffectGroupsService.AllAsync();

            var viewModel = new SideEffectGroupListingViewModel { SideEffectGroups = serviceModel };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(SideEffectGroupFormModel model)
        {
            var dbModel = Mapper.Map<SideEffectGroup>(model);

            var userId = this.userManager.GetUserId(User);
            dbModel.AdminId = userId;
            dbModel.DateOfAddition = DateTime.UtcNow;

            var successfulCreation = await this.adminSideEffectGroupsService.CreateAsync(dbModel);

            if (!successfulCreation)
            {
                ModelState.AddModelError(WebConstants.StatusMessage, WebConstants.SideEffectGroupNameExists);
                return View(model);
            }

            TempData.AddSuccessMessage($"Side effect group {model.Name} successfully created.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int sideEffectGroupId)
        {
            var dbModel = await this.adminSideEffectGroupsService.GetByIdAsync(sideEffectGroupId);

            if (dbModel == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<SideEffectGroupFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Update(SideEffectGroupFormModel model)
        {
            var dbModel = Mapper.Map<SideEffectGroup>(model);

            var successfulEditing = await this.adminSideEffectGroupsService.UpdateAsync(dbModel);

            if (!successfulEditing)
            {
                ModelState.AddModelError(WebConstants.StatusMessage, WebConstants.SideEffectGroupNameExists);
                return View(model);
            }

            TempData.AddSuccessMessage($"Side effect group {model.Name} successfully updated.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int sideEffectGroupId)
        {
            var dbModel = await this.adminSideEffectGroupsService.GetByIdAsync(sideEffectGroupId);

            if (dbModel == null)
            {
                return NotFound();
            }

            await this.adminSideEffectGroupsService.DeleteAsync(dbModel);

            TempData.AddSuccessMessage($"Side effect group {dbModel.Name} successfully deleted.");
            return RedirectToAction(nameof(Index));
        }
    }
}
