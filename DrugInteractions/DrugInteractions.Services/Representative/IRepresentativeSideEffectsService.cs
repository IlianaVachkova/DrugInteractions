using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Representative.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Representative
{
    public interface IRepresentativeSideEffectsService
    {
        Task<IEnumerable<RepresentativeSideEffectListingServiceModel>> AllAsync();

        Task CreateAsync(SideEffect model);
    }
}
