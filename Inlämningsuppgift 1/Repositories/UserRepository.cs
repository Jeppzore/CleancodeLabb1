using Inlämningsuppgift_1.Models;

namespace Inlämningsuppgift_1.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<User> Users = new()
        {
            new User { Id = 1, Username = "alice", Password = "password", Email = "alice@example.com" },
            new User { Id = 2, Username = "bob", Password = "password", Email = "bob@example.com" }
        };

        private static readonly Dictionary<string, int> Tokens = new Dictionary<string, int>();
        
        public Task<User?> Create(User user)
        {
            user.Id = Users.Max(u => u.Id) + 1;
            Users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> GetById(int id)
            => Task.FromResult(Users.FirstOrDefault(u => u.Id == id));
        

        public Task<User?> GetByUsername(string username)
            => Task.FromResult(Users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)));
    }
}
