using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
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

        [HttpPut]
        [Route("insert")]
        public IActionResult Insert([FromBody] ExpeditionLog insertData, [FromQuery] string login)
        {
            if(insertData == null)
                return BadRequest("Inserted data was null");

            var user = _databaseAccess.UserRepository.GetByLogin(login);
            if (user == null)
                return BadRequest($"Unable to find user with login = {login}");

            insertData.UserId = user.UserId;

            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.Insert(insertData);
                if (result)
                    return Json(result);
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

        [HttpGet]
        [Route("getlastest")]
        public IActionResult GetLastest() {
            try
            {
                var lastest = _databaseAccess.ExpeditionLogRepository.GetLastest();
                if(lastest == null)
                    return NotFound();

                return Json(lastest);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] ExpeditionLog updateData)
         {
            if (updateData == null)
                return BadRequest("updateData was null");
            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.Update(updateData);
                if (result)
                    return Json(result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update expeditionLog");
        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            if (id == 0)
                return BadRequest("id was 0");
            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.Delete(id);
                if (result)
                    return Json(result);
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
        [Route("getexpeditionlogsbylogin")]
        public IActionResult GetByUserLogin([FromQuery] string login)
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

        [HttpPost]
        [Route("checkifexpeditionlogexists")]
        public IActionResult CheckIfExpeditionLogExsists([FromBody] userLoginWithRoute request )
        {
            if (request == null )
                return BadRequest("Request was null");

            var user = _databaseAccess.UserRepository.GetByLogin(request.login);

            if (user == null)
                return BadRequest($"Unable to find user with login {request.login}");

            var result = _databaseAccess.ExpeditionLogRepository.CheckIfExists(request.routeId, user.UserId);

            return Json(result);
        }

        [HttpPost]
        [Route("deletebyloginandroute")]
        public IActionResult DeleteByLoginAndRoute([FromBody] userLoginWithRoute request) {
            if (request == null)
                return BadRequest("Request was null");

            var user = _databaseAccess.UserRepository.GetByLogin(request.login);
            if (user == null)
                return BadRequest($"Unable to find user with login {request.login}");

            var result = _databaseAccess.ExpeditionLogRepository.DeleteByUserAndRoute(request.routeId, user.UserId);

            if (result)
                return Json(result);
            return BadRequest("Unable to delete expeditionLog");

        }

        [HttpPost]
        [Route("getuserstats")]
        public IActionResult GetUserStats([FromQuery] string login)
        {
            if (login == null)
                return BadRequest("Login was null");

            var user = _databaseAccess.UserRepository.GetByLogin(login);

            if (user == null)
                return BadRequest($"Unable to find user with login = {login}");

            try
            {
                var result = _databaseAccess.ExpeditionLogRepository.GetUserStats(user.UserId);
                return Json(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
