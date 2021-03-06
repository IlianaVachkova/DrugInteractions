﻿using DrugInteractions.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services
{
    public interface IDrugService
    {
        Task<IEnumerable<DrugListingServiceModel>> FindByNameAsync(string searchText);

        Task<IEnumerable<DrugListingServiceModel>> FindByBrandAsync(string searchText);

        Task<IEnumerable<DrugListingServiceModel>> FindByDrugGroupAsync(string searchText);

        Task<IEnumerable<DrugListingServiceModel>> FindByRepresentativeAsync(string searchText);

        Task<IEnumerable<DrugListingServiceModel>> GetWeeklyDrugs();

        Task<IEnumerable<DrugChartServiceModel>> GetDrugsWithLessSideEffects(int drugsCount);
    }
}
