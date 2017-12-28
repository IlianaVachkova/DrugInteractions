using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task CreateAsync(SideEffectGroup model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }
    }
}
