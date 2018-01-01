using DrugInteractions.Data;
using DrugInteractions.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace DrugInteractions.Services.Implementations
{
    public class DrugService : IDrugService
    {
        private readonly DrugInteractionsDbContext db;

        public DrugService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<DrugListingServiceModel>> FindByNameAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Drugs
                .OrderByDescending(d => d.Id)
                .Where(d => d.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<DrugListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<DrugListingServiceModel>> FindByBrandAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Drugs
                .OrderByDescending(d => d.Id)
                .Where(d => d.Brand.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<DrugListingServiceModel>()
                .ToListAsync();
        }
    }
}
