using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Repr.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Repr
{
    public interface IReprDrugsService
    {
        Task<bool> CreateAsync(Drug model);

        Task<bool> UpdateAsync(Drug model);

        Task<Drug> GetByIdAsync(int id);

        Task<IEnumerable<ReprDrugListingServiceModel>> AllAsync();

        Task DeleteAsync(Drug model);

        Task SideEffectsInDrug(IEnumerable<int> sideEffectIds, int drugId);
    }
}
