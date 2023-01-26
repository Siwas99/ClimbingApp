using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using ClimbingApp.Contracts.Repositories;
using Route = ClimbingApp.Models.Route;

namespace ClimbingApp.Controllers
{
    [Route("api/Routes")]
    [EnableCors("AllowAll")]
    public class RouteController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public RouteController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPut]
        [Route("insert")]
        public IActionResult Insert([FromBody] Route insertData, [FromQuery] int rockId)
        {
            if (insertData == null || rockId == 0)
                return BadRequest("insertedData was null or rockId was 0");
            try
            {
                insertData.RockId = rockId;
                var result = _databaseAccess.RouteRepository.Insert(insertData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to insert route");
        }


        [HttpGet]
        [Route("getRoutes")]
        public IActionResult List()
        {
            var result = _databaseAccess.RouteRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Route updateData)
        {
            if (updateData == null)
                return BadRequest("updateData was null");

            try
            {
                var result = _databaseAccess.RouteRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update route");

        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int routeId)
        {
            try
            {
                var result = _databaseAccess.RouteRepository.Delete(routeId);
                if(result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete route with id = {routeId}");

        }

        [HttpPost]
        [Route("getroutebyid")]
        public IActionResult GetById([FromQuery] int? routeId = null)
        {
            if (routeId == null)
                return BadRequest("routeId was null");
            var result = _databaseAccess.RouteRepository.GetById(routeId.Value);

            return Json(result);
        }

        [HttpPost]
        [Route("getroutesbyrockid")]
        public IActionResult GetByRockId([FromQuery] int? rockId=null)
        {
            if (rockId == null)
                return BadRequest("RockId can't be null");
            var result = _databaseAccess.RouteRepository.GetByRockId(rockId.Value);

            return Json(result);
        }

    }
}
