using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.Users;
using DrugInteractions.Services.Admin;
using DrugInteractions.Test.DataHelpers;
using DrugInteractions.Test.Mocks;
using DrugInteractions.Web;
using DrugInteractions.Web.Areas.Admin.Controllers;
using DrugInteractions.Web.Areas.Admin.Models.DrugGroups;
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
    public class DrugGroupsControllerTests
    {
        public DrugGroupsControllerTests()
        {
            Tests.Initialize();
        }

        [Fact]
        public void DrugGroupsControllerShouldBeInTheAdminArea()
        {
            // Arrange
            var controlller = typeof(DrugGroupsController);

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
        public void DrugGroupsControllerShouldBeOnlyForAdminUsers()
        {
            // Arrange
            var controller = typeof(DrugGroupsController);

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
            var brands = DataHelper.GetDrugGroupsCollection();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.AllAsync())
                .ReturnsAsync(brands);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);

            // Act
            var result = await controller.Index();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            var viewName = result.As<ViewResult>().ViewName;
            viewName.Should().BeNull();
            var model = result.As<ViewResult>().Model;
            var viewModel = model.As<DrugGroupListingViewModel>();
            viewModel.Should().NotBeNull();
        }

        [Fact]
        public void GetCreateShouldReturnView()
        {
            // Arrange
            var controller = new DrugGroupsController(null, null);

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
            var resultDrugGroup = new DrugGroup();
            var drugGroupFormModel = DataHelper.GetDrugGroupFormModel();
            string successMessage = null;
            var userManager = this.GetUserManagerMock();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.CreateAsync(It.IsAny<DrugGroup>()))
                .Callback((DrugGroup model) => { resultDrugGroup = model; })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
               .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
               .Callback((string key, object message) => successMessage = message as string);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, userManager.Object);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Create(drugGroupFormModel);

            // Assert
            resultDrugGroup.Should().NotBeNull();
            resultDrugGroup.Name.Should().Be(drugGroupFormModel.Name);
            resultDrugGroup.AdminId.Should().Be(drugGroupFormModel.AdminId);

            successMessage.Should().Be($"Drug group {drugGroupFormModel.Name} successfully created.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task PostCreateShouldReturnsViewWithCorrectModelWhenDbThrowsException()
        {
            // Arrange
            var drugGroupFormModel = DataHelper.GetDrugGroupFormModel();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();
            var userManager = this.GetUserManagerMock();
            var mockExc = new Exception("Drug group with this name already exists.");

            adminDrugGroupsService
                .Setup(s => s.CreateAsync(It.IsAny<DrugGroup>())).ThrowsAsync(mockExc);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, userManager.Object);

            // Act
            var result = await controller.Create(drugGroupFormModel);

            // Assert
            result.Should().NotBeNull();
            controller.ModelState[string.Empty].Errors[0].ErrorMessage.Should().Be(mockExc.Message);
        }


        [Fact]
        public async Task GetUpdateShouldReturnViewWithValidModel()
        {
            // Arrange
            var drugGroupDb = DataHelper.GetDrugGroup();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(drugGroupDb);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);

            // Act
            var result = await controller.Update(drugGroupDb.Id);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
            result.As<ViewResult>().ViewName.Should().BeNull();
            var model = result.As<ViewResult>().Model;
            model.Should().BeOfType<DrugGroupFormModel>();
            var formModel = model.As<DrugGroupFormModel>();
            formModel.Id.Should().Be(drugGroupDb.Id);
            formModel.Name.Should().Be(drugGroupDb.Name);
            formModel.Admin.Name.Should().Be(drugGroupDb.Admin.Name);
            formModel.AdminId.Should().Be(drugGroupDb.AdminId);
        }

        [Fact]
        public async Task GetUpdateReturnsNotFoundWhenDbReturnsNull()
        {
            // Arrange
            var drugGroupId = 1;
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);

            // Act
            var result = await controller.Update(drugGroupId);

            // Assert
            result.Should().NotBeNull();
            result.As<NotFoundResult>().StatusCode.Should().Be(WebConstants.StatusCodeNotFound);
        }

        [Fact]
        public async Task PostUpdateShouldReturnRedirectWithValidModel()
        {
            // Arrange
            var resultDrugGroup = new DrugGroup();
            string successMessage = null;
            var drugGroupFormModel = DataHelper.GetDrugGroupFormModel();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.UpdateAsync(It.IsAny<DrugGroup>()))
                .Callback((DrugGroup model) => { resultDrugGroup = model; })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
               .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
               .Callback((string key, object message) => successMessage = message as string);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Update(drugGroupFormModel);

            // Assert
            resultDrugGroup.Should().NotBeNull();
            resultDrugGroup.Name.Should().Be(drugGroupFormModel.Name);
            resultDrugGroup.AdminId.Should().Be(drugGroupFormModel.AdminId);

            successMessage.Should().Be($"Drug group {drugGroupFormModel.Name} successfully updated.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task PostUpdateShouldReturnsViewWithCorrectModelWhenDbThrowsException()
        {
            // Arrange
            var drugGroupFormModel = DataHelper.GetDrugGroupFormModel();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();
            var mockExc = new Exception("Drug group with this name already exists.");

            adminDrugGroupsService
                .Setup(s => s.UpdateAsync(It.IsAny<DrugGroup>())).ThrowsAsync(mockExc);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);

            // Act
            var result = await controller.Update(drugGroupFormModel);

            // Assert
            result.Should().NotBeNull();
            controller.ModelState[string.Empty].Errors[0].ErrorMessage.Should().Be(mockExc.Message);
        }

        [Fact]
        public async Task DeleteReturnRedirectWithValidId()
        {
            // Arrange
            string successMessage = null;
            var resultDrugGroup = new DrugGroup();
            var drugGroupDb = DataHelper.GetDrugGroup();
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(drugGroupDb);

            adminDrugGroupsService
                .Setup(s => s.DeleteAsync(It.IsAny<DrugGroup>()))
                .Callback((DrugGroup model) => { resultDrugGroup = model; })
                .Returns(Task.CompletedTask);

            var tempData = new Mock<ITempDataDictionary>();
            tempData
               .SetupSet(t => t[WebConstants.TempDataSuccessMessageKey] = It.IsAny<string>())
               .Callback((string key, object message) => successMessage = message as string);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);
            controller.TempData = tempData.Object;

            // Act
            var result = await controller.Delete(drugGroupDb.Id);

            // Assert
            successMessage.Should().Be($"Drug group {drugGroupDb.Name} successfully deleted.");

            result.Should().BeOfType<RedirectToActionResult>();

            result.As<RedirectToActionResult>().ActionName.Should().Be("Index");
        }

        [Fact]
        public async Task DeleteShouldReturnsNotFoundWhenDbReturnsNull()
        {
            // Arrange
            var drugGroupId = 1;
            var adminDrugGroupsService = new Mock<IAdminDrugGroupsService>();

            adminDrugGroupsService
                .Setup(s => s.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new DrugGroupsController(adminDrugGroupsService.Object, null);

            // Act
            var result = await controller.Delete(drugGroupId);

            // Assert
            result.Should().NotBeNull();
            result.As<NotFoundResult>().StatusCode.Should().Be(WebConstants.StatusCodeNotFound);
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
