using DrugInteractions.Data.Models.Users;
using DrugInteractions.Web.Infrastructure.Populators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using static DrugInteractions.Web.WebConstants;

namespace DrugInteractions.Web.Areas.Representative.Controllers
{
    [Area(RepresentativeArea)]
    [Authorize(Roles = RepresentativeRole)]
    public class BaseRepresentativeController : Controller
    {
        protected readonly UserManager<User> userManager;

        protected readonly IDropDownListPopulator populator;

        public BaseRepresentativeController(UserManager<User> userManager, IDropDownListPopulator populator)
        {
            this.userManager = userManager;
            this.populator = populator;
        }
    }
}
