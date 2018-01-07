using DrugInteractions.Services.Models;
using System.Threading.Tasks;

namespace DrugInteractions.Services
{
    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string id);
    }
}
