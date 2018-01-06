using DrugInteractions.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services
{
    public interface IBrandService
    {
        Task<IEnumerable<BrandChartServiceModel>> GetBrandsWithMostDrugs(int brandsCount);
    }
}
