using AutoMapper;
using ClimbingApp.Contracts.Repositories;
using ClimbingApp.Data.DTO;
using ClimbingApp.Models;
using ClimbingApp.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ClimbingApp.Controllers
{
    [Route("api/app")]
    [EnableCors("AllowAll")]
    public class AppController : Controller
    {
        private readonly IDatabaseRepository _databaseAccess;

        public AppController(IDatabaseRepository databaseAccess)
        {
            _databaseAccess = databaseAccess;
        }

        [HttpPost]
        [Route("search")]
        public IActionResult Search([FromQuery] string phrase) 
        {
            if(phrase == null)
                return BadRequest("Phrase was null");

            try
            {
                var searchResult = new SearchResult();
                searchResult.Regions = _databaseAccess.RegionRepository.Search(phrase);
                searchResult.Areas = _databaseAccess.AreaRepository.Search(phrase);
                searchResult.Rocks = _databaseAccess.RockRepository.Search(phrase);
                searchResult.Routes = _databaseAccess.RouteRepository.Search(phrase);

                if (searchResult.Regions.IsNullOrEmpty() && searchResult.Areas.IsNullOrEmpty() && searchResult.Rocks.IsNullOrEmpty() && searchResult.Routes.IsNullOrEmpty())
                    return NoContent();
                    
                return Json(searchResult);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    } 
}
