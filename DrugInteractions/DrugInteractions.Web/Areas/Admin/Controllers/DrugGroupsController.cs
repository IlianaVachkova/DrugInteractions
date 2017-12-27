using AutoMapper;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.DrugGroups;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            var resultModel = new DrugGroupListingViewModel { DrugGroups = serviceModel };

            return View(resultModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddDrugGroupFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbModel = Mapper.Map<DrugGroup>(model);

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Admin = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;

            await this.adminDrugGroupsService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? drugGroupId)
        {
            var dbModel =await this.adminDrugGroupsService.GetById(drugGroupId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<AddDrugGroupFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddDrugGroupFormModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var dbModel = Mapper.Map<DrugGroup>(model);

            await this.adminDrugGroupsService.UpdateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
