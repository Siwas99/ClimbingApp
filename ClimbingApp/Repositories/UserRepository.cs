using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;


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
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Email = user.Email;
                //existingUser.Password = user.Password;
                existingUser.Login = user.Login;

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

    }
}
