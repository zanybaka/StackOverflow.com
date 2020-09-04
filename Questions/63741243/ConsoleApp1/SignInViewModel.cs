using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleApp1
{
    public class SignInViewModel
    {
        public string EmailEntry { get; set; }
        public string PasswordEntry { get; set; }
        public ICommand LoginCommand { get; }
        public readonly IUserService _userService;
        public readonly IPageService _pageService;

        public SignInViewModel(IUserService userService, IPageService pageService)
        {
            _userService = userService;
            _pageService = pageService;
            LoginCommand = new MyCommand(async () => await LogIn());
        }

        private async Task LogIn()
        {
            var user = _userService.LoginUser(EmailEntry);

            if (user == null)
            {
                await _pageService.DisplayAlert("Alert", "Invalid Credentials", "Ok");
                return;
            }

            if (user.Password != PasswordEntry)
            {
                await _pageService.DisplayAlert("Alert", "Invalid Credentials", "Ok");
                return;
            }

            PasswordEntry = "";
            await _pageService.PushAsync(new HomePage(user));
        }
    }
}