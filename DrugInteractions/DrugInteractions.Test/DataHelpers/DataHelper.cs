using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin.Models;
using DrugInteractions.Web.Areas.Admin.Models.Brands;
using DrugInteractions.Web.Areas.Admin.Models.DrugGroups;
using System;
using System.Collections.Generic;

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

        internal static Brand GetBrand()
        {
            return new Brand
            {
                Name = "Brand name",
                WebSite = "brand website",
                Admin = GetUser(),
                AdminId = GetUser().Id
            };
        }

        internal static BrandFormModel GetBrandFormModel()
        {
            return new BrandFormModel
            {
                Name = "Brand name",
                WebSite = "brand website",
               // Admin = GetUser(),
               // AdminId = GetUser().Id
            };
        }

        internal static IEnumerable<AdminBrandsListingServiceModel> GetBrandsCollection()
        {
            var brands = new List<AdminBrandsListingServiceModel>();

            for (int i = 1; i <= 10; i++)
            {
                brands.Add(new AdminBrandsListingServiceModel
                {
                    Id = i,
                    Name = "Brand" + i,
                    WebSite = "website" + i
                });
            }

            return brands;
        }

        internal static DrugGroup GetDrugGroup()
        {
            return new DrugGroup
            {
                Name="Drug group name",
                Admin = GetUser(),
                AdminId = GetUser().Id
            };
        }

        internal static DrugGroupFormModel GetDrugGroupFormModel()
        {
            return new DrugGroupFormModel
            {
                Name = "Drug group name",
                Admin = GetUser(),
                AdminId = GetUser().Id
            };
        }

        internal static IEnumerable<AdminDrugGroupListingServiceModel> GetDrugGroupsCollection()
        {
            var drugGroups = new List<AdminDrugGroupListingServiceModel>();

            for (int i = 1; i <= 10; i++)
            {
                drugGroups.Add(new AdminDrugGroupListingServiceModel
                {
                    Id=i,
                    Name="Drug group" + i
                });
            }

            return drugGroups;
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
