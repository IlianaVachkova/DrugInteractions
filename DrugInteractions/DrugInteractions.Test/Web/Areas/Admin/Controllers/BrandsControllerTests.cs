using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Test.DataHelpers;
using DrugInteractions.Test.Mocks;
using DrugInteractions.Web;
using DrugInteractions.Web.Areas.Admin.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace DrugInteractions.Test.Web.Areas.Admin.Controllers
{
    public class BrandsControllerTests
    {
        public BrandsControllerTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public void BrandsControllerShouldBeInTheAdminArea()
        {
            // Arrange
            var controlller = typeof(BrandsController);

            // Act
            var areaAttribute = controlller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AreaAttribute))
                as AreaAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.RouteValue.Should().Be(WebConstants.AdminArea);
        }

        [Fact]
        public void BrandsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(BrandsController);

            // Act
            var areaAttribute = controller
                .GetCustomAttributes(true)
                .FirstOrDefault(a => a.GetType() == typeof(AuthorizeAttribute))
                as AuthorizeAttribute;

            // Assert
            areaAttribute.Should().NotBeNull();
            areaAttribute.Roles.Should().Be(WebConstants.AdministratorRole);
        }

        [Fact]
        public void GetCreateShouldReturnView()
        {
            // Arrange
            var controller = new BrandsController(null, null);

            // Act
            var result = controller.Create();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            var viewName = result.As<ViewResult>().ViewName;
            viewName.Should().BeNull();
        }

        [Fact]
        public async Task PostCreateShouldReturnRedirectWithValidModel()
        {
            // Arrange
            var resultBrand = new Brand();
            var brandFormModel = DataHelper.GetBrandFormModel();
            string successMessage = null;
            var userManager = this.GetUserManagerMock();
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.CreateAsync(It.IsAny<Brand>()))
                .Callback((Brand model) => { resultBrand = model; })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
               .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
               .Callback((string key, object message) => successMessage = message as string);

            var controller = new BrandsController(adminBrandsService.Object, userManager.Object);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Create(brandFormModel);

            // Assert
            resultBrand.Should().NotBeNull();
            resultBrand.Name.Should().Be(brandFormModel.Name);
            resultBrand.WebSite.Should().Be(brandFormModel.WebSite);
            resultBrand.AdminId.Should().Be(brandFormModel.AdminId);

            successMessage.Should().Be($"Brand {brandFormModel.Name} successfully created.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
            result.As<RedirectToActionResult>().ControllerName.Should().Be("Home");
            result.As<RedirectToActionResult>().RouteValues.Keys.Should().Contain("area");
            result.As<RedirectToActionResult>().RouteValues.Values.Should().Contain(string.Empty);
        }

        [Fact]
        public async Task PostCreateShouldReturnsViewWithCorrectModelWhenDbThrowsException()
        {
            // Arrange
            var brandFormModel = DataHelper.GetBrandFormModel();
            var adminBrandsService = new Mock<IAdminBrandsService>();
            var userManager = this.GetUserManagerMock();
            var mockExc = new Exception("Brand with this name already exists.");

            adminBrandsService
                .Setup(s => s.CreateAsync(It.IsAny<Brand>())).ThrowsAsync(mockExc);

            var controller = new BrandsController(adminBrandsService.Object, userManager.Object);

            // Act
            var result =await controller.Create(brandFormModel);

            // Assert
            result.Should().NotBeNull();
            controller.ModelState[string.Empty].Errors[0].ErrorMessage.Should().Be(mockExc.Message);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var userManager = UserManagerMock.New;
            var user = DataHelper.GetUser();

            userManager
                .Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns(user.Id);

            return userManager;
        }
    }
}
