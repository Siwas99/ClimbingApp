﻿using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Schema;

using User = ClimbingApp.Models.User;

namespace ClimbingApp.Controllers
{
    [Route("api/Auth")]
    [EnableCors("AllowAll")]
    public class AuthController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;
        private readonly IConfiguration _configuration;
        public AuthController(IDatabaseRepository databaseAccess, IConfiguration configuration)
        {
            _databaseAccess = databaseAccess;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody] UserDTO user)
        {
            if(user == null)
            {
                return BadRequest("User was null");
            }
            _databaseAccess.UserRepository.CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

            try
            {
                //TODO Create validator
                var email = _databaseAccess.UserRepository.GetByEmail(user.Email);
                if (email != null)
                    return BadRequest("Użytkownik z takim adresem email już istnieje w bazie");

                var login = _databaseAccess.UserRepository.GetByLogin(user.Login);
                if (login != null)
                    return BadRequest("Użytkownik z taką nazwą użytkownika już istnieje w bazie");        

                var result = _databaseAccess.UserRepository.Register(user, passwordHash, passwordSalt);
                if (!result)
                    return BadRequest("Unable to register user");
                
                
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest($"Coś poszło nie tak podczas rejestracji nowego użytkownika, proszę skontaktuj się z administratorem. \n Exception: {ex}");
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserDTO request = null)
        {
            if (request == null || request.Login.Equals("") || request.Password.Equals(""))
                return BadRequest("Request, password or login was null");
            try
            {
                var user = _databaseAccess.UserRepository.GetByLogin(request.Login);
                if (user == null)
                    return BadRequest("Nie znaleziono użytkownika");

                if (!_databaseAccess.UserRepository.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    return BadRequest("Wprowadzono niepoprawne hasło");

                string token = _databaseAccess.UserRepository.GenerateToken(user, _configuration);

                return Ok(Json(new List<string> {token, user.Role.Name }));
            }
            catch(Exception ex)
            {
                return BadRequest($"Something went wrong during attmept to login in.Exception: {ex}");
            }
        }
    }
}
