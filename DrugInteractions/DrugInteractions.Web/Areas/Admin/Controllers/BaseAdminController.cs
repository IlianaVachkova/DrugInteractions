using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DrugInteractions.Data.Models.Users;

using static DrugInteractions.Web.WebConstants;

namespace DrugInteractions.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public class BaseAdminController : Controller
    {
        protected readonly UserManager<User> userManager;

        public BaseAdminController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }
    }
}
