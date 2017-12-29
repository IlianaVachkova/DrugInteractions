using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Representative.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<IEnumerable<RepresentativeSideEffectListingServiceModel>> AllAsync()
        {
            return await this.db
                .SideEffects
                .ProjectTo<RepresentativeSideEffectListingServiceModel>()
                .ToListAsync();
        }

        public async Task CreateAsync(SideEffect model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }
    }
}
