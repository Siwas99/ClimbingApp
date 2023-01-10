using AutoMapper;
using Azure.Core;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ClimbingApp.Controllers
{
    [Route("api/wishlist")]
    [EnableCors("AllowAll")]
    public class WishlistController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public WishlistController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPost]
        [Route("insert")]

        public IActionResult Insert( [FromBody] Models.Route route, [FromQuery] string login )
        {
            if (route == null )
            {
                return BadRequest("route or login was null");
            }

            var user = _databaseAccess.UserRepository.GetByLogin(login);
            if (user != null)
            {
                try
                {
                    var result = _databaseAccess.WishlistRepository.Insert(route, user);
                    if (result)
                        return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
                return BadRequest("Unable to insert wishlist");
            }
            return BadRequest($"Unable to find user with login = {login}");

        }


        [HttpGet]
        [Route("getRoles")]
        public IActionResult List()
        {
            var result = _databaseAccess.WishlistRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Wishlist updateData)
        {
            try
            {
                var result = _databaseAccess.WishlistRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update wishlist");

        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _databaseAccess.WishlistRepository.Delete(id);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete wishlist with id = {id}");

        }

        [HttpPost]
        [Route("getwishlistbyid")]
        public IActionResult GetById([FromBody] int id)
        {
            var result = _databaseAccess.WishlistRepository.GetById(id);

            return Json(result);
        }

        [HttpPost]
        [Route("getwishlistbylogin")]
        public IActionResult GetByLogin([FromBody] string? login = null)
        {
            if (login == null)
                return BadRequest("login was null");
            var user = _databaseAccess.UserRepository.GetByLogin(login);
            if (user == null)
                return BadRequest($"Unable to find user with login = {login}");

            var result = _databaseAccess.WishlistRepository.GetByUserId(user.UserId);

            return Json(result);
        }
    }
}
