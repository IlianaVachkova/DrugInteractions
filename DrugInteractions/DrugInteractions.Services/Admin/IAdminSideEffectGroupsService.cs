using DrugInteractions.Data.Models.SideEffects;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminSideEffectGroupsService
    {
        Task CreateAsync(SideEffectGroup model);
    }
}
