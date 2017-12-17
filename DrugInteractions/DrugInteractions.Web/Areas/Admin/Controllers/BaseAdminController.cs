using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static DrugInteractions.Web.WebConstants;

namespace DrugInteractions.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratorRole)]
    public class BaseAdminController : Controller
    {
    }
}
