using AutoMapper;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Repr;
using DrugInteractions.Web.Areas.Repr.Models.SideEffects;
using DrugInteractions.Web.Infrastructure.Populators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var model = new AddSideEffectFormModel
            {
                SideEffectGroups = await this.populator.GetSideEffectGroups()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddSideEffectFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.SideEffectGroups = await this.populator.GetSideEffectGroups();
                return View(model);
            }

            var dbModel = Mapper.Map<SideEffect>(model);

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Admin = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;

            await this.reprSideEffectService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? sideEffectId)
        {
            var dbModel = await this.reprSideEffectService.GetByIdAsync(sideEffectId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<AddSideEffectFormModel>(dbModel);

            viewModel.SideEffectGroups =await this.populator.GetSideEffectGroups();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddSideEffectFormModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var dbModel = Mapper.Map<SideEffect>(model);

            await this.reprSideEffectService.UpdateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
