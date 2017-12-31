using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Repr.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Repr
{
    public interface IReprDrugsService
    {
        Task CreateAsync(Drug model);

        Task UpdateAsync(Drug model);

        Task<Drug> GetByIdAsync(int? id);

        Task<IEnumerable<ReprDrugListingServiceModel>> AllAsync();

        Task DeleteAsync(Drug model);
    }
}
