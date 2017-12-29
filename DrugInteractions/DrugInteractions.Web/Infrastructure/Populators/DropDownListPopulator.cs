using DrugInteractions.Services.Admin;
using DrugInteractions.Services.Caching;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Infrastructure.Populators
{
    public class DropDownListPopulator : IDropDownListPopulator
    {
        private readonly IAdminSideEffectGroupsService sideEffectGroups;

        private readonly ICacheService cache;

        public DropDownListPopulator(ICacheService cache, IAdminSideEffectGroupsService sideEffectGroups)
        {
            this.cache = cache;
            this.sideEffectGroups = sideEffectGroups;
        }

        public async Task<IEnumerable<SelectListItem>> GetSideEffectGroups()
        {
            var allSideEffectGroups =await this.sideEffectGroups.AllAsync();

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
    }
}
