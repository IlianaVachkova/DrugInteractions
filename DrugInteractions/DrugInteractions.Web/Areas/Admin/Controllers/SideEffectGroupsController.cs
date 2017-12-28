using AutoMapper;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.SideEffectGroups;
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
        public async Task<IActionResult> Create(AddSideEffectGroupFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
       
            var dbModel = Mapper.Map<SideEffectGroup>(model);
       
            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Admin = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;
       
            await this.adminSideEffectGroupsService.CreateAsync(dbModel);
       
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? sideEffectGroupId)
        {
            var dbModel = await this.adminSideEffectGroupsService.GetByIdAsync(sideEffectGroupId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<AddSideEffectGroupFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddSideEffectGroupFormModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var dbModel = Mapper.Map<SideEffectGroup>(model);

            await this.adminSideEffectGroupsService.UpdateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
