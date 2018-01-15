using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminSideEffectGroupsService
    {
        Task<IEnumerable<AdminSideEffectGroupsListingServiceModel>> AllAsync();

        Task<bool> CreateAsync(SideEffectGroup model);

        Task<bool> UpdateAsync(SideEffectGroup model);

        Task<SideEffectGroup> GetByIdAsync(int id);

        Task DeleteAsync(SideEffectGroup model);
    }
}
