using AutoMapper;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.DrugGroups;
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
        public async Task<IActionResult> Create(AddDrugGroupFormModel model)
        {
            var dbModel = Mapper.Map<DrugGroup>(model);

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Admin = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;

            await this.adminDrugGroupsService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? drugGroupId)
        {
            var dbModel =await this.adminDrugGroupsService.GetByIdAsync(drugGroupId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<AddDrugGroupFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Update(AddDrugGroupFormModel model)
        {
            var dbModel = Mapper.Map<DrugGroup>(model);

            await this.adminDrugGroupsService.UpdateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? drugGroupId)
        {
            var dbModel = await this.adminDrugGroupsService.GetByIdAsync(drugGroupId);

            if (dbModel==null)
            {
                return BadRequest();
            }

            await this.adminDrugGroupsService.DeleteAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
