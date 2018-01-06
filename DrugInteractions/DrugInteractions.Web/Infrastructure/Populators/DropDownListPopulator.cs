using DrugInteractions.Services.Admin;
using DrugInteractions.Services.Caching;
using DrugInteractions.Services.Repr;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Infrastructure.Populators
{
    public class DropDownListPopulator : IDropDownListPopulator
    {
        private readonly IAdminSideEffectGroupsService sideEffectGroups;

        private readonly IAdminBrandsService brandsService;

        private readonly IAdminDrugGroupsService drugGroupsService;

        private readonly IReprSideEffectsService sideEffectsService;

        private readonly ICacheService cache;

        public DropDownListPopulator(ICacheService cache, IAdminSideEffectGroupsService sideEffectGroups, IAdminBrandsService brandsService, IAdminDrugGroupsService drugGroupsService, IReprSideEffectsService sideEffectsService)
        {
            this.cache = cache;
            this.sideEffectGroups = sideEffectGroups;
            this.brandsService = brandsService;
            this.drugGroupsService = drugGroupsService;
            this.sideEffectsService = sideEffectsService;
        }

        public async Task<IEnumerable<SelectListItem>> GetSideEffectGroups()
        {
            var allSideEffectGroups = await this.sideEffectGroups.AllAsync();

            var sideEffectGroups = this.cache.Get<IEnumerable<SelectListItem>>("sideEffectGroups",
                () =>
                {
                    return allSideEffectGroups
                    .Select(seffgr => new SelectListItem
                    {
                        Value = seffgr.Id.ToString(),
                        Text = seffgr.Name
                    })
                    .ToList();
                });

            return sideEffectGroups;
        }

        public async Task<IEnumerable<SelectListItem>> GetDrugGroups()
        {
            var allDrugGroups = await this.drugGroupsService.AllAsync();

            var drugGroups = this.cache.Get<IEnumerable<SelectListItem>>("drugGroups",
                () =>
                {
                    return allDrugGroups
                    .Select(dgr => new SelectListItem
                    {
                        Value = dgr.Id.ToString(),
                        Text = dgr.Name
                    })
                    .ToList();
                });

            return drugGroups;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            var allBrands = await this.brandsService.AllAsync();

            var brands = this.cache.Get<IEnumerable<SelectListItem>>("brands",
                () =>
                {
                    return allBrands
                    .Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Name
                    })
                    .ToList();
                });

            return brands;
        }

        public async Task<IEnumerable<SelectListItem>> GetSideEffects()
        {
            var allSideEffects = await this.sideEffectsService.AllAsync();

            var sideEffects = this.cache.Get<IEnumerable<SelectListItem>>("sideEffects",
                () =>
                {
                    return allSideEffects
                    .Select(seff => new SelectListItem
                    {
                        Value = seff.Id.ToString(),
                        Text = seff.Name
                    })
                    .ToList();
                });

            return sideEffects;
        }
    }
}
