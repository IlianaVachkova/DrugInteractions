using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Users;

namespace DrugInteractions.Services.Admin.Models
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
