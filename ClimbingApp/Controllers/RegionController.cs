using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Models;

using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace ClimbingApp.Controllers
{
    [Route("api/regions")]
    [EnableCors("AllowAll")]
    public class RegionController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public RegionController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPost]
        [Route("insert")]
        public IActionResult Insert([FromBody] Region insertData)
        {
            try
            {
                var result = _databaseAccess.RegionRepository.Insert(insertData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to insert region");

        }


        [HttpGet]
        [Route("getRegions")]
        public IActionResult List()
        {
            var result = _databaseAccess.RegionRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] Region updateData)
        {
            try
            {
                var result = _databaseAccess.RegionRepository.Update(updateData);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update region");
        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete([FromQuery] int regionId)
        {
            try
            {
                var result = _databaseAccess.RegionRepository.Delete(regionId);
                if(result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete region with id = {regionId}");
        }

        [HttpPost]
        [Route("getRegionbyid")]
        public IActionResult GetById([FromQuery] int? regionId = null)
        {
            if (regionId == null)
                return BadRequest("regionId was null");
            var result = _databaseAccess.RegionRepository.GetById(regionId.Value);

            return Json(result);
        }
    }
}
