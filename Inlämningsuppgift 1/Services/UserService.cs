using Inlämningsuppgift_1.Dtos;
using Inlämningsuppgift_1.Repositories;

namespace Inlämningsuppgift_1.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileDto?> GetProfile(int id)
        {
            var user = await _repository.GetById(id);
            if (user == null) return null;

            return new UserProfileDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
