using DrugInteractions.Data.Models.SideEffects;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Representative
{
    public interface IRepresentativeSideEffectsService
    {
        Task CreateAsync(SideEffect model);
    }
}
