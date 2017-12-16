using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.SideEffects;

namespace DrugInteractions.Data.Models.IntermediateTables
{
    public class DrugSideEffect
    {
        public int DrugId { get; set; }

        public Drug Drug { get; set; }

        public int SideEffectId { get; set; }

        public SideEffect SideEffect { get; set; }
    }
}