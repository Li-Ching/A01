using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebAPItest.Dtos;
using WebAPItest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnituresController : ControllerBase
    {
        private readonly A01Context _a01Context;

        public FurnituresController(A01Context a01Context)
        {
            _a01Context = a01Context;
        }

        // GET: api/<FurnituresController>
        [HttpGet]
        public IEnumerable<FurnituresDto> Get(string? Type)
        {
            var result = (from a in _a01Context.Furnitures
                          .Include(a => a.Brand)
                          select new FurnituresDto
                          {
                              FurnitureId=a.FurnitureId,
                              Type=a.Type,
                              Color=a.Color,
                              Style=a.Style,
                              Brand1 = (a.Brand==null) ? null: a.Brand.Brand1,
                              PhoneNumber = (a.Brand == null) ? null : a.Brand.PhoneNumber,
                              Address= (a.Brand == null) ? null : a.Brand.Address,
                              Logo= (a.Brand == null) ? null : a.Brand.Logo,
                              Location=a.Location,
                              Picture=a.Picture
                          });

            if (!string.IsNullOrWhiteSpace(Type))
            {
                result=result.Where(a => a.Type.Contains(Type));
            }

            return result;
        }

        // GET api/<FurnituresController>/5
        [HttpGet("{FurnitureId}")]
        public FurnituresDto Get(int FurnitureId)
        {
            var result = (from a in _a01Context.Furnitures
                          where a.FurnitureId== FurnitureId
                          select new FurnituresDto
                          {
                              FurnitureId=a.FurnitureId,
                              Type=a.Type,
                              Color=a.Color,
                              Style=a.Style,
                              Brand1= (a.Brand == null) ? null : a.Brand.Brand1,
                              PhoneNumber= (a.Brand == null) ? null : a.Brand.PhoneNumber,
                              Address= (a.Brand == null) ? null : a.Brand.Address,
                              Logo= (a.Brand == null) ? null : a.Brand.Logo,
                              Location=a.Location,
                              Picture=a.Picture
                          }).SingleOrDefault();
            return result;
        }

        // GET api/<FurnituresController>/5
        [HttpGet("Search")]
        public IEnumerable<FurnituresDto> Search(string content)
        {
            var result = (from a in _a01Context.Furnitures
                          .Include(a => a.Brand)
                          select new FurnituresDto
                          {
                              FurnitureId=a.FurnitureId,
                              Type=a.Type,
                              Color=a.Color,
                              Style=a.Style,
                              Brand1 = (a.Brand==null) ? null : a.Brand.Brand1,
                              PhoneNumber = (a.Brand == null) ? null : a.Brand.PhoneNumber,
                              Address= (a.Brand == null) ? null : a.Brand.Address,
                              Logo= (a.Brand == null) ? null : a.Brand.Logo,
                              Location=a.Location,
                              Picture=a.Picture
                          });

            if (!string.IsNullOrWhiteSpace(content))
            {
                result=result.Where(a => a.Type.Contains(content)||
                                        a.Color.Contains(content)||
                                        a.Style.Contains(content)||
                                        a.Brand1.Contains(content));
            }
            return result;
        }


        // POST api/<FurnituresController>
        /*[HttpPost]
        public ActionResult<Furniture> Post([FromBody] Furniture value)
        {
            _a01Context.Furnitures.Add(value);
            _a01Context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { furnitureId = value.FurnitureId }, value);

        }*/

        /*// PUT api/<FurnituresController>/5
        [HttpPut("{FurnitureId}")]
        public IActionResult Put(int furnitureId, [FromBody] Furniture value)
        {
            if (furnitureId != value.FurnitureId)
            {
                return BadRequest();
            }

            _a01Context.Entry(value).State = EntityState.Modified;

            try
            {
                _a01Context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (_a01Context.Furnitures.Any(f => f.FurnitureId == furnitureId))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }
            return NoContent();
        
        }

        // DELETE api/<FurnituresController>/5
        [HttpPost("{FurnitureId}")]
        public IActionResult Delete(int FurnitureId)
        {
            var delete = _a01Context.Furnitures.Find(FurnitureId);
            if (delete == null)
            {
                return NotFound();
            }
            _a01Context.Furnitures.Remove(delete);
            _a01Context.SaveChanges();
            return NoContent();
        }*/
    }
}
