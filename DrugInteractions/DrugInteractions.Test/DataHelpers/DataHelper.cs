using DrugInteractions.Data.Models.Users;
using DrugInteractions.Web.Areas.Admin.Models.Brands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrugInteractions.Test.DataHelpers
{
    internal static class DataHelper
    {
        internal static List<User> GetUsersCollection()
        {
            var users = new List<User>
            {
                new User { Id = "1", UserName = "First" },
                new User { Id = "2", UserName = "Second" }
            };

            return users;
        }

        internal static BrandFormModel GetBrandFormModel()
        {
            return new BrandFormModel
            {
                Name = "Brand name",
                WebSite = "brand website",
                Admin = GetUser(),
                AdminId = GetUser().Id
            };
        }

        internal static User GetUser()
        {
            return new User
            {
                Id = "1",
                Name = "Name",
                Facebook = "facebook.com",
                LinkedIn = "linkedin.com",
                BirthDate = new DateTime(1990, 12, 12),
                DateOfAddition = new DateTime(2017, 12, 12)
            };
        }
    }
}
