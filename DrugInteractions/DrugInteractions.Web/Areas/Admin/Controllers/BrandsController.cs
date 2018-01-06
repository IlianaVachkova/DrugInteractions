using AutoMapper;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Web.Areas.Admin.Models.Brands;
using DrugInteractions.Web.Infrastructure.Extensions;
using DrugInteractions.Web.Infrastructure.Filters;
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
        [ValidateModelState]
        public async Task<IActionResult> Create(BrandFormModel model)
        {
            var dbModel = Mapper.Map<Brand>(model);

            var userId = this.userManager.GetUserId(User);
            dbModel.AdminId = userId;
            dbModel.DateOfAddition = DateTime.UtcNow;

            try
            {
                await this.adminBrandsService.CreateAsync(dbModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Brand with this name already exists.");
                return View(model);
            }

            TempData.AddSuccessMessage($"Brand {model.Name} successfully created.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int brandId)
        {
            var dbModel = await this.adminBrandsService.GetByIdAsync(brandId);

            if (dbModel == null)
            {
                return NotFound();
            }

            var viewModel = Mapper.Map<BrandFormModel>(dbModel);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateModelState]
        public async Task<IActionResult> Update(BrandFormModel model)
        {
            var dbModel = Mapper.Map<Brand>(model);

            try
            {
                await this.adminBrandsService.UpdateAsync(dbModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Brand with this name already exists.");
                return View(model);
            }

            TempData.AddSuccessMessage($"Brand {model.Name} successfully updated.");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int brandId)
        {
            var dbModel = await this.adminBrandsService.GetByIdAsync(brandId);

            if (dbModel == null)
            {
                return NotFound();
            }

            await this.adminBrandsService.DeleteAsync(dbModel);

            TempData.AddSuccessMessage($"Brand {dbModel.Name} successfully deleted.");
            return RedirectToAction(nameof(Index));
        }
    }
}
