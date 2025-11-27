using Inlämningsuppgift_1.Dtos.Users;
using Inlämningsuppgift_1.Models;
using Inlämningsuppgift_1.Repositories.Users;

namespace Inlämningsuppgift_1.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUserRepository _repository;
        private static readonly Dictionary<string, int> Tokens = new();

        public AuthService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<int?> GetUserIdFromToken(string token)
        {
            if (Tokens.TryGetValue(token, out var id))
                return Task.FromResult<int?>(id);

            return Task.FromResult<int?>(null);
        }

        public async Task<string?> Login(LoginRequest request)
        {
            var user = await _repository.GetByUsername(request.Username);
            if (user == null || user.Password != request.Password)
                return null;

            var token = Guid.NewGuid().ToString();
            Tokens[token] = user.Id;
            return token;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var exists = await _repository.GetByUsername(request.Username);
            if (exists != null) return false;

            var user = new User
            {
                Username = request.Username,
                Password = request.Password,
                Email = request.Email
            };

            await _repository.Create(user);
            return true;
        }
    }
}
