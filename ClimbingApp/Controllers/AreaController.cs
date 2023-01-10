using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ClimbingApp.Data.DTO;

namespace ClimbingApp.Controllers
{
    [Route("api/areas")]
    [EnableCors("AllowAll")]
    public class AreaController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public AreaController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPut]
        [Route("insert")]
        public IActionResult Insert([FromBody] Area area, [FromQuery] int? regionId = null)
        {
            if (regionId == null)
            {
                return BadRequest("regionId was null");
            }
            area.RegionId = regionId.Value;
            try
            {
                var result = _databaseAccess.AreaRepository.Insert(area);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to insert area");

        }


        [HttpGet]
        [Route("getAreas")]
        public IActionResult List()
        {
            var result = _databaseAccess.AreaRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("getareasbyregionid")]
        public IActionResult ListAreasByRegionId([FromQuery] int? regionId = null)
        {
            if (regionId == null)
                return BadRequest("RegionId was null");
            
            var result = _databaseAccess.AreaRepository.ListAreasByRegionId(regionId.Value);

            return Json(result);

        }

        [HttpPost]
        [Route("getareaswithnumberofroutesbyregionid")]
        //[Authorize]
        public IActionResult ListAreasWithNumberOfRoutesbyRegionId([FromQuery] int? regionId = null)
        {
            if (regionId == null)
                return BadRequest("RegionId was null");

            //var user = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = _databaseAccess.AreaRepository.ListAreasWithNumberOfRoutesByRegionId(regionId.Value);

            return Json(result);
        }



        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Area updateData, [FromQuery] int regionId)
        {
            if(regionId == null)
            {
                return BadRequest("regionId was null");
            }

            try
            {
                updateData.RegionId = regionId;

                var result = _databaseAccess.AreaRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update area");
        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int areaId)
        {
            try
            {
                var result = _databaseAccess.AreaRepository.Delete(areaId);
                if(result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete area with id = {areaId}");
        }

        [HttpPost]
        [Route("getareabyid")]
        public IActionResult GetById([FromQuery] int? areaId = null)
        {
            if (areaId == null)
                return BadRequest("areaId was null");
            var result = _databaseAccess.AreaRepository.GetById(areaId.Value);

            return Json(result);
        }


        [HttpPost]
        [Route("countroutesinarea")]
        public IActionResult CountRoutesInArea([FromQuery] int areaId)
        {
            var result = _databaseAccess.AreaRepository.CountRoutesInArea(areaId);

            return Json(result);
        }
    }
}
