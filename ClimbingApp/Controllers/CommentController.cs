using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingApp.Controllers
{
    [Route("api/comments")]
    [EnableCors("AllowAll")]
    public class CommentController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public CommentController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert([FromBody] Comment insertData)
        {
            try
            {
                var result = _databaseAccess.CommentRepository.Insert(insertData);
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
            var result = _databaseAccess.CommentRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Comment updateData)
        {
            try
            {
                var result = _databaseAccess.CommentRepository.Update(updateData);
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
                var result = _databaseAccess.CommentRepository.Delete(id);
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
        [Route("getcommentbyid")]
        public IActionResult GetById([FromBody] int id)
        {
            var result = _databaseAccess.CommentRepository.GetById(id);

            return Json(result);
        }
    }
}
