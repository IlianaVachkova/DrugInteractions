using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin.Implementations
{
    public class AdminBrandsService : IAdminBrandsService
    {
        private readonly DrugInteractionsDbContext db;

        public AdminBrandsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminBrandsListingServiceModel>> AllAsync()
        {
            return await this.db
                .Brands
                .ProjectTo<AdminBrandsListingServiceModel>()
                .ToListAsync();
        }

        public async Task CreateAsync(Brand model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Brand model)
        {
            this.db.Brands.Update(model);

            await this.db.SaveChangesAsync();
        }

        public async Task<Brand> GetByIdAsync(int? id)
        {
            return await this.db.Brands.FindAsync(id);
        }
    }
}
