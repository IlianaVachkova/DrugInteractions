using DrugInteractions.Data.Models.Drugs;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminDrugGroupsService
    {
        Task CreateAsync(DrugGroup model);
    }
}
