using DrugInteractions.Common.Mapping;
using DrugInteractions.Data.Models.Users;
using System;

namespace DrugInteractions.Services.Models
{
    public class UserProfileServiceModel : IMapFrom<User>
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Facebook { get; set; }

        public string LinkedIn { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DateOfAddition { get; set; }
    }
}
