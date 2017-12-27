using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin.Implementations
{
    public class AdminDrugGroupsService : IAdminDrugGroupsService
    {
        private readonly DrugInteractionsDbContext db;

        public AdminDrugGroupsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminDrugGroupListingServiceModel>> AllAsync()
        {
            return await this.db
                .DrugGroups
                .ProjectTo<AdminDrugGroupListingServiceModel>()
                .ToListAsync();
        }

        public async Task CreateAsync(DrugGroup model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(DrugGroup model)
        {
            this.db.DrugGroups.Update(model);

            await this.db.SaveChangesAsync();
        }

        public async Task<DrugGroup> GetByIdAsync(int? id)
        {
            return await this.db.DrugGroups.FindAsync(id);
        }

        public async Task DeleteAsync(DrugGroup model)
        {
            this.db.DrugGroups.Remove(model);

            await this.db.SaveChangesAsync();
        }
    }
}
