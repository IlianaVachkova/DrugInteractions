using AutoMapper;
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

        public async Task<IActionResult> Index()
        {
            var serviceModel = await adminBrandsService.AllAsync();

            var viewModel = new BrandListingViewModel { Brands = serviceModel };

            return View(viewModel);
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

        public async Task<IActionResult> Update(int? brandId)
        {
            var dbModel = await this.adminBrandsService.GetByIdAsync(brandId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            var viewModel = Mapper.Map<AddBrandFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AddBrandFormModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var dbModel = Mapper.Map<Brand>(model);

            await this.adminBrandsService.UpdateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? brandId)
        {
            var dbModel = await this.adminBrandsService.GetByIdAsync(brandId);

            if (dbModel == null)
            {
                return BadRequest();
            }

            await this.adminBrandsService.DeleteAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
