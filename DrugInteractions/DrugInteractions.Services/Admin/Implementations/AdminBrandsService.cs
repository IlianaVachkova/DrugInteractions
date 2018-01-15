using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Services.Admin.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> CreateAsync(Brand model)
        {
            if (this.db.Brands.Any(b => b.Name == model.Name))
            {
                return false;
            }

            this.db.Add(model);

            await this.db.SaveChangesAsync();

            return true;
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

        public async Task DeleteAsync(Brand model)
        {
            this.db.Brands.Remove(model);

            await this.db.SaveChangesAsync();
        }
    }
}
