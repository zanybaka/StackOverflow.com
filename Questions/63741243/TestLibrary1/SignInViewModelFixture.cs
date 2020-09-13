using ConsoleApp1;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace TestLibrary1
{
    [TestFixture]
    public class Fixture
    {
        private Mock<IUserService> _userService;
        private Mock<IPageService> _pageService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _pageService = new Mock<IPageService>();
        }

        [Test]
        public void LoginUserShouldBeCalled()
        {
            // Given
            var vm = CreateInstance();
            vm.EmailEntry = "email?";

            // When
            vm.LoginCommand.Execute(null);

            // Then
            _userService.Verify(x => x.LoginUser("email?"), Times.Once);
            _userService.VerifyNoOtherCalls();
        }

        [Test]
        public void DisplayAlertShouldBeCalledForNotFoundUser()
        {
            // Given
            var vm = CreateInstance();
            vm.EmailEntry = "not@found.user";
            _userService.Setup(x => x.LoginUser("not@found.user")).Returns((User)null).Verifiable();

            // When
            vm.LoginCommand.Execute(null);

            // Then
            _userService.Verify();
            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), "Invalid Credentials", It.IsAny<string>()));
            _pageService.VerifyNoOtherCalls();
        }

        [Test]
        public void DisplayAlertShouldBeCalledForInvalidPassword()
        {
            // Given
            var vm = CreateInstance();
            vm.EmailEntry    = "test@email.ru";
            vm.PasswordEntry = "password";
            _userService.Setup(x => x.LoginUser(It.IsAny<string>())).Returns(new User(vm.EmailEntry, "ppppp")).Verifiable();

            // When
            vm.LoginCommand.Execute(null);

            // Then
            _userService.Verify();
            _pageService.Verify(x => x.DisplayAlert(It.IsAny<string>(), "Invalid Credentials", It.IsAny<string>()));
            _pageService.VerifyNoOtherCalls();
        }

        [Test]
        public void DisplayAlertShouldBeCalledForInvalidPassword2()
        {
            // Given
            var vm = CreateInstance();
            vm.EmailEntry    = "test@email.ru";
            vm.PasswordEntry = "password";
            User user = new User(vm.EmailEntry, vm.PasswordEntry);
            _userService.Setup(x => x.LoginUser(It.IsAny<string>())).Returns(user).Verifiable();

            // When
            vm.LoginCommand.Execute(null);

            // Then
            _userService.Verify();
            _pageService.Verify(x => x.PushAsync(It.Is<HomePage>(y => y.User == user)));
            _pageService.VerifyNoOtherCalls();
            vm.PasswordEntry.Should().BeEmpty();
        }

        private SignInViewModel CreateInstance()
        {
            return new SignInViewModel(_userService.Object, _pageService.Object);
        }
    }
}
