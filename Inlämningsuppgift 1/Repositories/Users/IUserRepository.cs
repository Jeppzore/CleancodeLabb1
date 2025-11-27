using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories.Users
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<User?> GetByUsername(string username);
        Task<User?> Create(User user);
    }
}
