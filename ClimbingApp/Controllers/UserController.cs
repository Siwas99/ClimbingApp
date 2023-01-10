using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using ClimbingApp.Contracts.Repositories;
using User = ClimbingApp.Models.User;

namespace ClimbingApp.Controllers
{
    [Route("api/users")]
    [EnableCors("AllowAll")]
    public class UserController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public UserController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
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
        public IActionResult Update([FromBody] User updateData)
        {
            try
            {
                var result = _databaseAccess.UserRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update user");

        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int id)
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
    }
}
