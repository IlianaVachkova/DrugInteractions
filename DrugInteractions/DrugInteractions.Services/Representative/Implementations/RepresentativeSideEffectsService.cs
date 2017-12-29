using DrugInteractions.Data;
using DrugInteractions.Data.Models.SideEffects;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Representative.Implementations
{
    public class RepresentativeSideEffectsService : IRepresentativeSideEffectsService
    {
        private readonly DrugInteractionsDbContext db;

        public RepresentativeSideEffectsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(SideEffect model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }
    }
}
