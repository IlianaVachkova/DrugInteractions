using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.IntermediateTables;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Repr.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> CreateAsync(SideEffect model)
        {
            if (this.db.SideEffects.Any(seff => seff.Name == model.Name))
            {
                return false;
            }

            this.db.Add(model);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(SideEffect model)
        {
            if (this.db.SideEffects.Any(seff => seff.Name == model.Name && seff.Id != model.Id))
            {
                return false;
            }

            this.db.SideEffects.Update(model);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<SideEffect> GetByIdAsync(int id)
        {
            return await this.db.SideEffects.FindAsync(id);
        }

        public async Task DeleteAsync(SideEffect model)
        {
            this.db.SideEffects.Remove(model);

            await this.db.SaveChangesAsync();
        }

        public async Task DrugsInSideEffect(IEnumerable<int> drugIds, int sideEffectId)
        {
            foreach (var dId in drugIds)
            {
                this.db.Add(new DrugSideEffect { DrugId = dId, SideEffectId = sideEffectId });
            }

            await this.db.SaveChangesAsync();
        }
    }
}
