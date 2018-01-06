using DrugInteractions.Data;
using DrugInteractions.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<IEnumerable<DrugListingServiceModel>> FindByDrugGroupAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Drugs
                .OrderByDescending(d => d.Id)
                .Where(d => d.DrugGroup.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<DrugListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<DrugListingServiceModel>> FindByRepresentativeAsync(string searchText)
        {
            searchText = searchText ?? string.Empty;

            return await this.db
                .Drugs
                .OrderByDescending(d => d.Id)
                .Where(d => d.Representative.Name.ToLower().Contains(searchText.ToLower()))
                .ProjectTo<DrugListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<DrugListingServiceModel>> GetWeeklyDrugs()
        {
            var dateBeforeWeek = DateTime.UtcNow.AddDays(-7);

            return await this.db
                .Drugs
                .OrderByDescending(d => d.Id)
                .Where(d => d.DateOfAddition >= dateBeforeWeek)
                .ProjectTo<DrugListingServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<DrugChartServiceModel>> GetDrugsWithLessSideEffects(int drugsCount)
        {
            var drugsList = await this.db
                .Drugs
                .OrderByDescending(d => d.SideEffects.Count)
                .ProjectTo<DrugChartServiceModel>()
                .Take(drugsCount)
                .ToListAsync();

            if (drugsList.Count < drugsCount)
            {
                while (drugsList.Count < drugsCount)
                {
                    drugsList.Add(new DrugChartServiceModel { Name = "default name", SideEffectsCont = 1 });
                }
            }

            return drugsList;
        }
    }
}
