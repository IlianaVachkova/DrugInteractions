using DrugInteractions.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
