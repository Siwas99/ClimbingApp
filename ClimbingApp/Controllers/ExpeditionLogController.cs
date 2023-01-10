using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters;

namespace ClimbingApp.Controllers
{
    [Route("api/expeditionlogs")]
    [EnableCors("AllowAll")]
    public class ExpeditionLogController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public ExpeditionLogController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert([FromBody] ExpeditionLog insertData)
        {
            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.Insert(insertData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to add expedition log");

        }


        [HttpGet]
        [Route("getexpeditionLogs")]
        public IActionResult List()
        {
            var result = _databaseAccess.ExpeditionLogRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] ExpeditionLog updateData)
        {
            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update expeditionLog");
        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromBody] int id)
        {
            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.Delete(id);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete expeditionLog with id = {id}");
        }

        [HttpPost]
        [Route("getexpeditionlogbyid")]
        public IActionResult GetById([FromBody] int id)
        {
            var result = _databaseAccess.ExpeditionLogRepository.GetById(id);

            return Json(result);
        }

        [HttpPost]
        [Route("getexpeditionlogbylogin")]
        public IActionResult GetByUserLogin([FromBody] string? login = null)
        {
            if(login == null)
            {
                return BadRequest("Login was null");
            }

            var user = _databaseAccess.UserRepository.GetByLogin(login);

            if(user == null)
            {
                return BadRequest($"Unable to find user with login {login}");
            }

            var result = _databaseAccess.ExpeditionLogRepository.GetByUsersId(user.UserId);

            return Json(result);
        }
    }
}
