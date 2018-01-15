using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminDrugGroupsService
    {
        Task<IEnumerable<AdminDrugGroupListingServiceModel>> AllAsync();

        Task<bool> CreateAsync(DrugGroup model);

        Task<bool> UpdateAsync(DrugGroup model);

        Task DeleteAsync(DrugGroup model);

        Task<DrugGroup> GetByIdAsync(int id);
    }
}
