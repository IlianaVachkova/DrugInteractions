using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.SideEffects;

namespace DrugInteractions.Services.Repr.Model
{
    public class ReprSideEffectListingServiceModel : IMapFrom<SideEffect>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public SideEffectGroup SideEffectGroup { get; set; }
    }
}
