using DrugInteractions.Data.Models.Drugs;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Repr
{
    public interface IReprDrugsService
    {
        Task CreateAsync(Drug model);

        Task UpdateAsync(Drug model);

        Task<Drug> GetByIdAsync(int? id);
    }
}
