using DrugInteractions.Data.Models.Brands;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminBrandsService
    {
        Task CreateAsync(Brand model);
    }
}
