using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItest.Dtos;
using WebAPItest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPItest.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly A01Context _a01Context;
        

        public FavoritesController(A01Context a01Context)
        {
            _a01Context = a01Context;
        }

        // GET: api/<FavoritesController>
        [HttpGet]
        public IEnumerable<FavoritesDto> Get()
        {
            var Claim = HttpContext.User.Claims.ToList();
            var UserId = Claim.Where(a => a.Type == "UserId").First().Value;
            var result = (from b in _a01Context.Favorites
                          where b.UserId== Int32.Parse(UserId)
                          select new FavoritesDto
                          {
                              UserId = b.UserId,
                              FurnitureId= b.FurnitureId
                          }).ToList();
            return result;
        }

        // GET api/<FavoritesController>/5
        [HttpGet("{userId}")]
        public IEnumerable<FavoritesDto> Get(int userId)
        {
            var result = (from b in _a01Context.Favorites
                          where b.UserId== userId
                          select new FavoritesDto
                          {
                              UserId = b.UserId,
                              FurnitureId= b.FurnitureId
                          }).ToList();
            return result;
        }

        // POST api/<FavoritesController>
        [HttpPost]
        public void Post([FromBody] FavoritesDto value)
        {
            var Claim = HttpContext.User.Claims.ToList();
            var UserId = Claim.Where(a => a.Type == "UserId").First().Value;
            Favorite insert = new Favorite
            {
                UserId=Int32.Parse(UserId),
                FurnitureId=value.FurnitureId
            };
            _a01Context.Favorites.Add(insert);
            _a01Context.SaveChanges();
        }

        // DELETE api/<FavoritesController>/5
        [HttpPost("{furnitureId}")]
        public IActionResult Delete(int furnitureId)
        {
            var Claim = HttpContext.User.Claims.ToList();
            var UserId = Claim.Where(a => a.Type == "UserId").First().Value;
            var delete = (from b in _a01Context.Favorites
                          where b.UserId== Int32.Parse(UserId)
                          & b.FurnitureId== furnitureId
                          select b).SingleOrDefault();
            if (delete == null)
            {
                return NotFound();
            }
            _a01Context.Favorites.Remove(delete);
            _a01Context.SaveChanges();
            return NoContent();
        }
    }
}
