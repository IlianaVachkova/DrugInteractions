using DrugInteractions.Data;
using DrugInteractions.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace DrugInteractions.Services.Implementations
{
    public class BrandService : IBrandService
    {
        private readonly DrugInteractionsDbContext db;

        public BrandService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<BrandChartServiceModel>> GetBrandsWithMostDrugs(int brandsCount)
        {
            var brandsList = await this.db
                .Brands
                .OrderByDescending(b => b.Drugs.Count)
                .ProjectTo<BrandChartServiceModel>()
                .Take(brandsCount)
                .ToListAsync();

            if (brandsList.Count < brandsCount)
            {
                while (brandsList.Count < brandsCount)
                {
                    brandsList.Add(new BrandChartServiceModel { Name = "default name", DrugsCount = 0 });
                }
            }

            return brandsList;
        }
    }
}
