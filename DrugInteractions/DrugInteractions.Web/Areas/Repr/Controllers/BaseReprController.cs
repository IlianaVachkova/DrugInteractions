using DrugInteractions.Data.Models.Users;
using DrugInteractions.Web.Infrastructure.Populators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static DrugInteractions.Web.WebConstants;

namespace DrugInteractions.Web.Areas.Repr.Controllers
{
    [Area(RepresentativeArea)]
    [Authorize(Roles = RepresentativeRole)]
    public class BaseReprController : Controller
    {
        protected readonly UserManager<User> userManager;

        protected readonly IDropDownListPopulator populator;

        public BaseReprController(UserManager<User> userManager, IDropDownListPopulator populator)
        {
            this.userManager = userManager;
            this.populator = populator;
        }
    }
}
