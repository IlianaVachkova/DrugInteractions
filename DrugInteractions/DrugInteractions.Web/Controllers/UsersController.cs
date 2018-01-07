using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DrugInteractions.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        private readonly UserManager<User> userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            this.userService = userService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            var profile = await this.userService.ProfileAsync(user.Id);

            return View(profile);
        }
    }
}
