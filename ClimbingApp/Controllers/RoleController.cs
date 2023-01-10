using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingApp.Controllers
{
    [Route("api/Roles")]
    [EnableCors("AllowAll")]
    public class RoleController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public RoleController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert([FromBody] Role insertData)
        {
            try
            {
                var result = _databaseAccess.RoleRepository.Insert(insertData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to delete role");
        }


        [HttpGet]
        [Route("getRoles")]
        public IActionResult List()
        {
            var result = _databaseAccess.RoleRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Role updateData)
        {
            try
            {
                var result = _databaseAccess.RoleRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update role");
        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _databaseAccess.RoleRepository.Delete(id);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete role with id = {id}");
        }

        [HttpPost]
        [Route("getRolebyid")]
        public IActionResult GetById([FromBody] int id)
        {
            var result = _databaseAccess.RoleRepository.GetById(id);

            return Json(result);
        }
    }
}
