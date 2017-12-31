using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Repr.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task CreateAsync(Drug model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Drug model)
        {
            this.db.Drugs.Update(model);

            await this.db.SaveChangesAsync();
        }

        public async Task<Drug> GetByIdAsync(int? id)
        {
            return await this.db.Drugs.FindAsync(id);
        }

        public async Task DeleteAsync(Drug model)
        {
            this.db.Drugs.Remove(model);

            await this.db.SaveChangesAsync();
        }
    }
}
