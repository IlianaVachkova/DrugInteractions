using DrugInteractions.Data.Models.Users;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace DrugInteractions.Test.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<User>> New
            => new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    }
}
