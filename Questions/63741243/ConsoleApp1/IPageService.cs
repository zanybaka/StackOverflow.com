using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IPageService
    {
        public Task PushAsync(Page page);
        Task DisplayAlert(string header, string message, string button);
    }
}