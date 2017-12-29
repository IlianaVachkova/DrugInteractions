﻿using AutoMapper;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Representative;
using DrugInteractions.Web.Areas.Representative.Models.SideEffects;
using DrugInteractions.Web.Infrastructure.Populators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Areas.Representative.Controllers
{
    public class SideEffectsController : BaseRepresentativeController
    {
        private readonly IRepresentativeSideEffectsService representativeSideEffectService;

        public SideEffectsController(IRepresentativeSideEffectsService representativeSideEffectService, UserManager<User> userManager, IDropDownListPopulator populator)
            : base(userManager, populator)
        {
            this.representativeSideEffectService = representativeSideEffectService;
        }

        public IActionResult Index()
        {
            return View();
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

            await this.representativeSideEffectService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
