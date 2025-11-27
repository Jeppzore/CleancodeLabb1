using Inlämningsuppgift_1.Dtos.Users;

namespace Inlämningsuppgift_1.Services
{
    public interface IAuthService
    {
        Task<string?> Login(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<int?> GetUserIdFromToken(string token);
    }
}
