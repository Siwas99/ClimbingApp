using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClimbingApp.Controllers
{
    [Route("api/rocks")]
    [EnableCors("AllowAll")]
    public class RockController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public RockController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPut]
        [Route("insert")]
        public IActionResult Insert([FromBody] RockDTO insertData, [FromQuery] int areaId)
        {
            if(insertData == null)
            {
                return BadRequest("Insertred data was null");
            }

            if(areaId == 0)
            {
                return BadRequest("AreaId was 0");
            }
    
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                    var rockExpo = _databaseAccess.RockFaceExposureRepository.GetRockFaceExposureByName(insertData.RockFaceExposure);
                    if(rockExpo == null)
                    {
                        transaction.Dispose();
                        return BadRequest("Unable to find rock face exposure");
                    }

                    var rock = new Rock
                    {
                        Name = insertData.Name,
                        Description = insertData.Description,
                        Distance = insertData.Distance,
                        Height = insertData.Height,
                        Popularity = insertData.Popularity,
                        isLoose = insertData.IsLoose,
                        isRecommended = insertData.IsRecommended,
                        isShadedFromTrees = insertData.IsShadedFromTrees,
                        positionLatitude = insertData.PositionLatitude,
                        positionLogitude = insertData.PositionLogitude,
                        AreaId = areaId,
                        RockFaceExposureId = rockExpo.RockFaceExposureId,
                        PhotoURL = ""
                    };

                    var result = _databaseAccess.RockRepository.Insert(rock);

                    if (!result)
                    {
                        transaction.Dispose();
                        return BadRequest("Unable to add rock");
                    }

                        var rockId = _databaseAccess.RockRepository.GetRockIdByName(insertData.Name);
                    if(rockId != 0)
                    {
                        if (insertData.Slabs)
                        {
                            if (!_databaseAccess.DominantRockFormationRepository.InsertByRockId(rockId, "Slabs"))
                            {
                                transaction.Dispose();
                                return BadRequest("Unable to add Dominant Rock Formation - slabs");
                            } 
                        }
                        if (insertData.Vertical)
                        {
                            if (!_databaseAccess.DominantRockFormationRepository.InsertByRockId(rockId, "Vertical"))
                            {
                                transaction.Dispose();
                                return BadRequest("Unable to add Dominant Rock Formation - vertical");
                            }
                        }
                        if (insertData.Overhang)
                        {
                            if (!_databaseAccess.DominantRockFormationRepository.InsertByRockId(rockId, "Overhang"))
                            {
                                transaction.Dispose();
                                return BadRequest("Unable to add Dominant Rock Formation - overhang");
                            }
                        }
                        if (insertData.Roof)
                        {
                            if (!_databaseAccess.DominantRockFormationRepository.InsertByRockId(rockId, "Roof"))
                            {
                                transaction.Dispose();
                                return BadRequest("Unable to add Dominant Rock Formation - roof");
                            }
                        }
                    }

                    transaction.Complete();
                    return Ok();
                    }
                    catch (Exception e)
                    {
                        return BadRequest(e);
                    }
                }
            return BadRequest("Unable to insert rock");
        }


        [HttpGet]
        [Route("getrocks")]
        public IActionResult List()
        {
            var result = _databaseAccess.RockRepository.List();

            return Json(result);

        }

        [HttpPost]
        [Route("getrocksbyregionid")]
        public IActionResult ListRocksByAreaId([FromQuery] int? areaId = null)
        {
            if (areaId == null)
                return BadRequest("RegionId was null");

            var result = _databaseAccess.RockRepository.ListRocksByAreaId(areaId.Value);

            return Json(result);

        }

        [HttpPost]
        [Route("getrockswithnumberofroutesbyregionid")]
        public IActionResult ListRocksWithNumberOfRoutesbyAreaId([FromQuery] int? areaId = null)
        {
            if (areaId == null)
                return BadRequest("RegionId was null");

            var result = _databaseAccess.RockRepository.ListRocksWithNumberOfRoutesByAreaId(areaId.Value);

            return Json(result);
        }


        [HttpPost]
        [Route("update")]
        public IActionResult Update([FromBody] RockDTO updateData)
        {
            try
            {
                var rockExpo = _databaseAccess.RockFaceExposureRepository.GetRockFaceExposureByName(updateData.RockFaceExposure);
                if (rockExpo == null)
                    return BadRequest("Unable to find rock face exposure");

                var rock = new Rock
                {
                    RockId =updateData.RockId,
                    Name = updateData.Name,
                    Description = updateData.Description,
                    Distance = updateData.Distance,
                    Height = updateData.Height,
                    Popularity = updateData.Popularity,
                    isLoose = updateData.IsLoose,
                    isRecommended = updateData.IsRecommended,
                    isShadedFromTrees = updateData.IsShadedFromTrees,
                    positionLatitude = updateData.PositionLatitude,
                    positionLogitude = updateData.PositionLogitude,
                    AreaId = updateData.AreaId,
                    RockFaceExposureId = rockExpo.RockFaceExposureId
                };

                var result = _databaseAccess.RockRepository.Update(rock);
                if (result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest("Unable to update rock");

        }


        [HttpPost]
        [Route("delete")]
        public IActionResult Delete(int rockId)
        {
            try
            {
                var result = _databaseAccess.RockRepository.Delete(rockId);
                if(result)
                    return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return BadRequest($"Unable to delete rock with id = {rockId}");

        }

        [HttpPost]
        [Route("getbyid")]
        public IActionResult GetById([FromQuery] int rockId)
        {
            var result = _databaseAccess.RockRepository.GetById(rockId);

            return Json(result);
        }

    }
}
