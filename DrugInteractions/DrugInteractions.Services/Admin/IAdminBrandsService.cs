using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminBrandsService
    {
        Task<IEnumerable<AdminBrandsListingServiceModel>> AllAsync();

        Task CreateAsync(Brand model);

        Task UpdateAsync(Brand model);

        Task<Brand> GetByIdAsync(int? id);
    }
}
