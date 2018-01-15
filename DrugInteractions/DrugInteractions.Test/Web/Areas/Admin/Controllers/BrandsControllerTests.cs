using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Test.DataHelpers;
using DrugInteractions.Test.Mocks;
using DrugInteractions.Web;
using DrugInteractions.Web.Areas.Admin.Controllers;
using DrugInteractions.Web.Areas.Admin.Models.Brands;
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
        public async Task IndexShouldReturnsViewWithCorrectModel()
        {
            // Arrange
            var brands = DataHelper.GetBrandsCollection();
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.AllAsync())
                .ReturnsAsync(brands);

            var controller = new BrandsController(adminBrandsService.Object, null);

            // Act
            var result = await controller.Index();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            var viewName = result.As<ViewResult>().ViewName;
            viewName.Should().BeNull();
            var model = result.As<ViewResult>().Model;
            var viewModel = model.As<BrandListingViewModel>();
            viewModel.Should().NotBeNull();
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
                .ReturnsAsync(true);

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

            successMessage.Should().Be($"Brand {brandFormModel.Name} successfully created.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task PostCreateShouldReturnsViewWithModelWhenInvalidModel()
        {
            // Arrange
            var resultBrand = new Brand();
            var brandFormModel = DataHelper.GetBrandFormModel();
            var adminBrandsService = new Mock<IAdminBrandsService>();
            var userManager = this.GetUserManagerMock();

            adminBrandsService
                .Setup(s => s.CreateAsync(It.IsAny<Brand>()))
                .Callback((Brand model) => { resultBrand = model; })
                .ReturnsAsync(false);

            var controller = new BrandsController(adminBrandsService.Object, userManager.Object);

            // Act
            var result = await controller.Create(brandFormModel);

            // Assert
            result.Should().NotBeNull();
            controller.ModelState[WebConstants.StatusMessage].Errors[0].ErrorMessage.Should().Be(WebConstants.BrandNameExists);
        }

        [Fact]
        public async Task GetUpdateShouldReturnViewWithValidModel()
        {
            // Arrange
            var brandDb = DataHelper.GetBrand();
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(brandDb);

            var controller = new BrandsController(adminBrandsService.Object, null);

            // Act
            var result = await controller.Update(brandDb.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().BeNull();
            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<BrandFormModel>();
            var formModel = model.As<BrandFormModel>();
            formModel.Id.Should().Be(brandDb.Id);
            formModel.Name.Should().Be(brandDb.Name);
            formModel.WebSite.Should().Be(brandDb.WebSite);
            formModel.Admin.Name.Should().Be(brandDb.Admin.Name);
            formModel.AdminId.Should().Be(brandDb.AdminId);
        }

        [Fact]
        public async Task GetUpdateReturnsNotFoundWhenDbReturnsNull()
        {
            // Arrange
            var brandId = 1;
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new BrandsController(adminBrandsService.Object, null);

            // Act
            var result = await controller.Update(brandId);

            // Assert
            result.Should().NotBeNull();
            result.As<NotFoundResult>().StatusCode.Should().Be(WebConstants.StatusCodeNotFound);
        }

        [Fact]
        public async Task PostUpdateShouldReturnRedirectWithValidModel()
        {
            // Arrange
            var resultBrand = new Brand();
            string successMessage = null;
            var brandFormModel = DataHelper.GetBrandFormModel();
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.UpdateAsync(It.IsAny<Brand>()))
                .Callback((Brand model) => { resultBrand = model; })
                .ReturnsAsync(true);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
               .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
               .Callback((string key, object message) => successMessage = message as string);

            var controller = new BrandsController(adminBrandsService.Object, null);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Update(brandFormModel);

            // Assert
            resultBrand.Should().NotBeNull();
            resultBrand.Name.Should().Be(brandFormModel.Name);
            resultBrand.WebSite.Should().Be(brandFormModel.WebSite);
            resultBrand.AdminId.Should().Be(brandFormModel.AdminId);

            successMessage.Should().Be($"Brand {brandFormModel.Name} successfully updated.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task PostUpdateShouldReturnsViewWithModelWhenInvalidModel()
        {
            // Arrange
            var resultBrand = new Brand();
            var brandFormModel = DataHelper.GetBrandFormModel();
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.UpdateAsync(It.IsAny<Brand>()))
                .Callback((Brand model) => { resultBrand = model; })
                .ReturnsAsync(false);

            var controller = new BrandsController(adminBrandsService.Object, null);

            // Act
            var result = await controller.Update(brandFormModel);

            // Assert
            result.Should().NotBeNull();
            controller.ModelState[WebConstants.StatusMessage].Errors[0].ErrorMessage.Should().Be(WebConstants.BrandNameExists);
        }

        [Fact]
        public async Task DeleteReturnRedirectWithValidId()
        {
            // Arrange
            string successMessage = null;
            var resultBrand = new Brand();
            var brandDb = DataHelper.GetBrand();
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(brandDb);

            adminBrandsService
                .Setup(s => s.DeleteAsync(It.IsAny<Brand>()))
                .Callback((Brand model) => { resultBrand = model; })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
               .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
               .Callback((string key, object message) => successMessage = message as string);

            var controller = new BrandsController(adminBrandsService.Object, null);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Delete(brandDb.Id);

            // Assert
            successMessage.Should().Be($"Brand {brandDb.Name} successfully deleted.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task DeleteShouldReturnsNotFoundWhenDbReturnsNull()
        {
            // Arrange
            var brandId = 1;
            var adminBrandsService = new Mock<IAdminBrandsService>();

            adminBrandsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new BrandsController(adminBrandsService.Object, null);

            // Act
            var result = await controller.Delete(brandId);

            // Assert
            result.Should().NotBeNull();
            result.As<NotFoundResult>().StatusCode.Should().Be(WebConstants.StatusCodeNotFound);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
           var userManager = UserManagerMock.New;
            var users = DataHelper.GetUsersCollection();

            userManager
               .Setup(u => u.GetUsersInRoleAsync(It.IsAny<string>()))
               .ReturnsAsync(users);

            userManager
               .Setup(u => u.GetUserId(It.IsAny<ClaimsPrincipal>()))
               .Returns(users.FirstOrDefault().Id);
           
           return userManager;
        }
    }
}
