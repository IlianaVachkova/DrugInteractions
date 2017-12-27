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

    }
}
