using System.ComponentModel.DataAnnotations;

namespace DrugInteractions.Web.Areas.Admin.Models.Users
{
    public class UserRoleFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
