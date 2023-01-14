using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using User = ClimbingApp.Models.User;

namespace ClimbingApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DataContext dbContext;
        private IDatabaseRepository databaseRepository;
        
        public UserRepository(DataContext dbContext, IDatabaseRepository databaseRepository)
        {
            this.dbContext = dbContext;
            this.databaseRepository = databaseRepository;
            
        }

        public bool Insert(User user)
        {
            try
            {
                dbContext.Users.Add(user);

                dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
            
        }

        public bool Register(UserDTO userDTO, byte[] passwordHash, byte[] passwordSalt)
        {
            var user = new Models.User
            {
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                Email = userDTO.Email,
                Login = userDTO.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2    
            };
            try
            {
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }


        public bool Update(User user)
        {
            var existingUser = GetById(user.UserId); 

            if(existingUser == null) 
                return false;

            try
            {
                if (!existingUser.Name.Equals(user.Name) && !String.IsNullOrEmpty(user.Name))
                    existingUser.Name = user.Name;
                
                if (!existingUser.Surname.Equals(user.Surname) && !String.IsNullOrEmpty(user.Surname))
                    existingUser.Surname = user.Surname;
                
                if (!existingUser.Email.Equals(user.Email) && !String.IsNullOrEmpty(user.Email))
                    existingUser.Email = user.Email;
                
                if (!existingUser.Login.Equals(user.Login) && !String.IsNullOrEmpty(user.Login))
                    existingUser.Login = user.Login;
                
                if (existingUser.PasswordHash != user.PasswordHash && !user.PasswordHash.IsNullOrEmpty())
                    existingUser.PasswordHash = user.PasswordHash;

                if (existingUser.PasswordSalt != user.PasswordSalt && !user.PasswordSalt.IsNullOrEmpty())
                    existingUser.PasswordSalt = user.PasswordSalt;

                dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            { 
                return false;
            }

        }

        public bool Delete(int id)
        {
            var existingUser = GetById(id);

            if(existingUser == null ) 
                return false;

            try
            {
                dbContext.Users.Remove(existingUser);
                dbContext.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }

        public List<User> List()
        {
            return dbContext.Users.ToList();
        }

        public User GetById(int userId)
        {
            return dbContext.Users.Where(x => x.UserId == userId).SingleOrDefault();
        }

        public User GetByLogin(string login)
        {
            return dbContext.Users.Include(r => r.Role).Where(x => x.Login.Equals(login)).SingleOrDefault();
        }
        public User GetByEmail(string email)
        {
            return dbContext.Users.Where(x => x.Email.Equals(email)).SingleOrDefault();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public string GenerateToken(User user, IConfiguration _configuration)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.Role, user.Role.RoleId == 1 ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(12),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public bool PromoteUser(string login)
        {
            try
            {
                var user = GetByLogin(login);
                if (user == null)
                    return false;

                user.RoleId = 1;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }

    }
}
