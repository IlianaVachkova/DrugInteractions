using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.IntermediateTables;
using DrugInteractions.Services.Repr.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Repr.Implementations
{
    public class ReprDrugsService : IReprDrugsService
    {
        private readonly DrugInteractionsDbContext db;

        public ReprDrugsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ReprDrugListingServiceModel>> AllAsync()
        {
            return await this.db
                .Drugs
                .ProjectTo<ReprDrugListingServiceModel>()
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(Drug model)
        {
            if (this.db.Drugs.Any(d => d.Name == model.Name))
            {
                return false;
            }

            this.db.Add(model);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(Drug model)
        {
            if (this.db.Drugs.Any(d => d.Name == model.Name && d.Id != model.Id))
            {
                return false;
            }

            this.db.Drugs.Update(model);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<Drug> GetByIdAsync(int id)
        {
            return await this.db.Drugs.FindAsync(id);
        }

        public async Task DeleteAsync(Drug model)
        {
            this.db.Drugs.Remove(model);

            await this.db.SaveChangesAsync();
        }

        public async Task SideEffectsInDrug(IEnumerable<int> sideEffectIds, int drugId)
        {
            foreach (var seffId in sideEffectIds)
            {
                this.db.Add(new DrugSideEffect { DrugId = drugId, SideEffectId = seffId });
            }

            await this.db.SaveChangesAsync();
        }
    }
}
