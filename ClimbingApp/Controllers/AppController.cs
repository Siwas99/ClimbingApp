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
using static System.Net.Mime.MediaTypeNames;

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
            if (phrase == null)
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("imagetest")]
        public IActionResult UploadImage([FromForm] RockDTO rock)
        {
            if (rock == null) 
                return BadRequest("rock was null");
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                //string sCurrentDirectory = Path.GetFullPath(Path.Combine(path, @"..\..\"));
                var fileName = $"{rock.Name}.jpg";
                var  newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", @"Frontend\climbing-app\public\img", fileName));
                Console.WriteLine(newPath);


                using (Stream stream = new FileStream(newPath, FileMode.Create))
                {
                    rock.Image.CopyTo(stream);


                }
                    return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("imagechange")]
        public  IActionResult ChangeImage([FromForm] RockDTO rock)
        {
            if (rock == null)
                return BadRequest("rock was null");

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                //string sCurrentDirectory = Path.GetFullPath(Path.Combine(path, @"..\..\"));
                var fileName = $"{rock.Name}.jpg";
                var newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\..\", @"Frontend\climbing-app\public\img", fileName));
                Console.WriteLine(newPath);

                if (System.IO.File.Exists(newPath))
                {
                    // usunięcie pliku
                    System.IO.File.Delete(newPath);
                }
                // zapisanie nowego pliku
                using (var fileStream = new FileStream(newPath, FileMode.Create))
                {
                    rock.Image.CopyToAsync(fileStream);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    } 
}
