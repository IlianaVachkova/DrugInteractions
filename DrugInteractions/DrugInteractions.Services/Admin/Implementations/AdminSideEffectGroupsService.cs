using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin.Implementations
{
    public class AdminSideEffectGroupsService : IAdminSideEffectGroupsService
    {
        private readonly DrugInteractionsDbContext db;

        public AdminSideEffectGroupsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminSideEffectGroupsListingServiceModel>> AllAsync()
        {
            return await this.db
                .SideEffectGroups
                .ProjectTo<AdminSideEffectGroupsListingServiceModel>()
                .ToListAsync();
        }

        public async Task<bool> CreateAsync(SideEffectGroup model)
        {
            if (this.db.SideEffectGroups.Any(seff=>seff.Name==model.Name))
            {
                return false;
            }

            this.db.Add(model);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(SideEffectGroup model)
        {
            this.db.SideEffectGroups.Update(model);

            if (this.db.SideEffectGroups.Any(seff => seff.Name == model.Name && seff.Id != model.Id))
            {
                return false;
            }

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<SideEffectGroup> GetByIdAsync(int id)
        {
            return await this.db.SideEffectGroups.FindAsync(id);
        }

        public async Task DeleteAsync(SideEffectGroup model)
        {
            this.db.SideEffectGroups.Remove(model);

            await this.db.SaveChangesAsync();
        }
    }
}
