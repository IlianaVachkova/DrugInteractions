using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using Microsoft.AspNetCore.Identity;

namespace DrugInteractions.Web.Areas.Admin.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IAdminUserService users;
        private readonly RoleManager<IdentityUser> roleManager;
        private readonly UserManager<User> userManager;

        public UsersController(IAdminUserService users, RoleManager<IdentityUser> roleManager, UserManager<User> userManager)
        {
            this.users = users;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
    }
}
