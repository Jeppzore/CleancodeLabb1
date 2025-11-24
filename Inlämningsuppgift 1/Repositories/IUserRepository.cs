using static Inlämningsuppgift_1.Services.UserService;

namespace Inlämningsuppgift_1.Repositories
{
    public interface IUserRepository
    {
        User? GetById(int id);
        User? GetByUsername(string username);
        void Add(User user);
    }
}
