using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Repr.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Repr.Implementations
{
    public class ReprSideEffectsService : IReprSideEffectsService
    {
        private readonly DrugInteractionsDbContext db;

        public ReprSideEffectsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ReprSideEffectListingServiceModel>> AllAsync()
        {
            return await this.db
                .SideEffects
                .ProjectTo<ReprSideEffectListingServiceModel>()
                .ToListAsync();
        }

        public async Task CreateAsync(SideEffect model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }
    }
}
