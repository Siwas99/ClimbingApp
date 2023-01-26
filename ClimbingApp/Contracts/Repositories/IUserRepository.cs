using ClimbingApp.Data.DTO;
using User = ClimbingApp.Models.User;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool Register(UserDTO userDTO, byte[] passwordHash, byte[] passwordSalt);
        public User GetByLogin(string login);
        public User GetByEmail(string email);
        public string GenerateToken(User user, IConfiguration _configuration);
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool PromoteUser(string login);
    }
}