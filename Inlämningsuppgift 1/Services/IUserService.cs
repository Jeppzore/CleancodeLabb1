using Inlämningsuppgift_1.Dtos;

namespace Inlämningsuppgift_1.Services
{
    public interface IUserService
    {
        Task<UserProfileDto?> GetProfile(int userId);
    }
}
