using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminDrugGroupsService
    {
        Task<IEnumerable<AdminDrugGroupListingServiceModel>> AllAsync();

        Task CreateAsync(DrugGroup model);

        Task UpdateAsync(DrugGroup model);

        Task DeleteAsync(DrugGroup model);

        Task<DrugGroup> GetByIdAsync(int? id);
    }
}
