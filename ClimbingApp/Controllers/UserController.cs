using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using ClimbingApp.Contracts.Repositories;
using User = ClimbingApp.Models.User;
using ClimbingApp.Data.DTO;

namespace ClimbingApp.Controllers
{
    [Route("api/users")]
    [EnableCors("AllowAll")]
    public class UserController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;
        private readonly IConfiguration _configuration;


        public UserController(IDatabaseRepository databaseAccess, IConfiguration configuration)
        {
            _databaseAccess = databaseAccess;
            _configuration = configuration;

        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert([FromBody] User insertData)
        {
            try
            {
                var result = _databaseAccess.UserRepository.Insert(insertData);
                if(result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to insert user");

        }


        [HttpGet]
        [Route("getusers")]
        public IActionResult List()
        {
            var result = _databaseAccess.UserRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] UserDTO updateData)
        {
            if (updateData == null)
                return BadRequest("updateData was null");

            try
            {
                _databaseAccess.UserRepository.CreatePasswordHash(updateData.Password, out byte[] passwordHash, out byte[] passwordSalt);
                if (String.IsNullOrEmpty(updateData.Password)) { 
                    passwordHash = null;
                    passwordSalt = null;
                }

                var user = new User
                {
                    UserId = updateData.userId,
                    Name = updateData.Name,
                    Surname = updateData.Surname,
                    Email = updateData.Email,
                    Login = updateData.Login,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                var result = _databaseAccess.UserRepository.Update(user);
                if (!result)
                    return Json(false) ;

                if(String.IsNullOrEmpty(updateData.Login))
                    return Json(true);


                var editedUser = _databaseAccess.UserRepository.GetByLogin(updateData.Login);
                var token = _databaseAccess.UserRepository.GenerateToken(editedUser, _configuration);

                return Ok(Json(new List<string> { token, editedUser.Login }));

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            try
            {
                var result = _databaseAccess.UserRepository.Delete(id);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete user with id = {id}");

        }

        [HttpPost]
        [Route("getuserbyid")]
        public IActionResult GetById([FromBody] int id)
        {
            var result = _databaseAccess.UserRepository.GetById(id);

            return Json(result);
        }

        [HttpPost]
        [Route("getbylogin")]
        public IActionResult GetByLogin([FromQuery] string login)
        {
            if (login == null)
                return BadRequest("login was null");

            try
            {
                var  user = _databaseAccess.UserRepository.GetByLogin(login);

                if (user == null)
                    return BadRequest($"Unable to find user with login = {login}");

                return Json(user);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("promoteuser")]
        public IActionResult PromoteUser([FromQuery] string login)
        {
            if (login == null)
                return BadRequest("login was null");

            try
            {
                var result = _databaseAccess.UserRepository.PromoteUser(login);

                if (result == null)
                    return Json(false);

                return Json(true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("deletebylogin")]
        public IActionResult DeleteByLogin([FromQuery] string login)
        {
            if (login == null)
                return BadRequest("Login was null");

            try
            {
                var user = _databaseAccess.UserRepository.GetByLogin(login);

                if (user == null)
                    return Json(false);

                var result = _databaseAccess.UserRepository.Delete(user.UserId);
                if (!result)
                    return Json(false);

                return Json(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
