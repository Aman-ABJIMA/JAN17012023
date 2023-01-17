using WebApplication6.Models;

namespace WebApplication6.Repository
{
    public interface ILinkRepository
    {
        //Task<string> GetLinkAsync(string link);
        Task<string> AddURLAsync(string url);
        Task<string> SearchLinkAsync(string url);
        // Task GetLinkByIdAsync(int id);
    }
}