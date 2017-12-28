﻿using AutoMapper;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.Brands;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Areas.Admin.Controllers
{
    public class BrandsController : BaseAdminController
    {
        private readonly IAdminBrandsService adminBrandsService;

        public BrandsController(IAdminBrandsService adminBrandsService, UserManager<User> userManager)
            : base(userManager)
        {
            this.adminBrandsService = adminBrandsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddBrandFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbModel = Mapper.Map<Brand>(model);

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Admin = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;

            await this.adminBrandsService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}