namespace ConsoleApp1
{
    public class HomePage : Page
    {
        public HomePage(User user)
        {
            User = user;
        }

        public User User { get; }
    }
}