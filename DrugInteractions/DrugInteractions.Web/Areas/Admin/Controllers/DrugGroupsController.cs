using AutoMapper;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.DrugGroups;
using DrugInteractions.Web.Infrastructure.Extensions;
using DrugInteractions.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Areas.Admin.Controllers
{
    public class DrugGroupsController : BaseAdminController
    {
        private readonly IAdminDrugGroupsService adminDrugGroupsService;

        public DrugGroupsController(IAdminDrugGroupsService adminDrugGroupsService, UserManager<User> userManager)
            : base(userManager)
        {
            this.adminDrugGroupsService = adminDrugGroupsService;
        }

        public async Task<IActionResult> Index()
        {
            var serviceModel = await adminDrugGroupsService.AllAsync();

            var viewModel = new DrugGroupListingViewModel { DrugGroups = serviceModel };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Create(DrugGroupFormModel model)
        {
            var dbModel = Mapper.Map<DrugGroup>(model);

            var userId = this.userManager.GetUserId(User);
            dbModel.AdminId = userId;
            dbModel.DateOfAddition = DateTime.UtcNow;

            var successfulCreation = await this.adminDrugGroupsService.CreateAsync(dbModel);

            if (!successfulCreation)
            {
                ModelState.AddModelError(WebConstants.StatusMessage, WebConstants.DrugGroupNameExists);
                return View(model);
            }

            TempData.AddSuccessMessage($"Drug group {model.Name} successfully created.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int drugGroupId)
        {
            var dbModel =await this.adminDrugGroupsService.GetByIdAsync(drugGroupId);

            if (dbModel == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<DrugGroupFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Update(DrugGroupFormModel model)
        {
            var dbModel = Mapper.Map<DrugGroup>(model);

            var successfulEditing = await this.adminDrugGroupsService.UpdateAsync(dbModel);

            if (!successfulEditing)
            {
                ModelState.AddModelError(WebConstants.StatusMessage, WebConstants.DrugGroupNameExists);
                return View(model);
            }

            TempData.AddSuccessMessage($"Drug group {model.Name} successfully updated.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int drugGroupId)
        {
            var dbModel = await this.adminDrugGroupsService.GetByIdAsync(drugGroupId);

            if (dbModel==null)
            {
                return NotFound();
            }

            await this.adminDrugGroupsService.DeleteAsync(dbModel);

            TempData.AddSuccessMessage($"Drug group {dbModel.Name} successfully deleted.");
            return RedirectToAction(nameof(Index));
        }
    }
}
