﻿using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using User = ClimbingApp.Models.User;

namespace ClimbingApp.Contracts.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public bool Register(UserDTO userDTO, byte[] passwordHash, byte[] passwordSalt);
        public User GetByLogin(string login);
        public User GetByEmail(string email);
    }
}