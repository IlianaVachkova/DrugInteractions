using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Repr;
using DrugInteractions.Web.Infrastructure.Populators;
using DrugInteractions.Web.Areas.Repr.Models.Drugs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AutoMapper;
using DrugInteractions.Data.Models.Drugs;

namespace DrugInteractions.Web.Areas.Repr.Controllers
{
    public class DrugsController : BaseReprController
    {
        private readonly IReprDrugsService reprDrugsService;

        public DrugsController(IReprDrugsService reprDrugsService, UserManager<User> userManager, IDropDownListPopulator populator)
            : base(userManager, populator)
        {
            this.reprDrugsService = reprDrugsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var model = new AddDrugFormModel
            {
                DrugGroups = await this.populator.GetDrugGroups(),
                Brands = await this.populator.GetBrands()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddDrugFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DrugGroups = await this.populator.GetDrugGroups();
                model.Brands = await this.populator.GetBrands();
                return View(model);
            }

            var dbModel = Mapper.Map<Drug>(model);

            var currentUser = await userManager.GetUserAsync(HttpContext.User);
            dbModel.Representative = currentUser;
            dbModel.DateOfAddition = DateTime.UtcNow;

            await this.reprDrugsService.CreateAsync(dbModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
