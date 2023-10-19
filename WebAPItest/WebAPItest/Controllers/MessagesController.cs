using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPItest.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly A01Context _a01Context;
        public MessagesController(A01Context a01Context)
        {
            _a01Context = a01Context;
        }
        // GET: api/<MessagesController>
        [HttpGet]
        public IEnumerable<Models.Message> Get()
        {
            return _a01Context.Messages;
        }

        // GET api/<MessagesController>/5
        [HttpGet("{FurnitureId}")]
        public IEnumerable<Models.Message> Get(int FurnitureId)
        {
            var result = _a01Context.Messages.Where(n => n.FurnitureId == FurnitureId);
            if (result == null)
            {
                return (IEnumerable<Models.Message>)NotFound("此家具沒有留言");
            }
            return result;
        }

        /*// POST api/<MessagesController>
        [HttpPost]
        public IActionResult Post([FromBody] Brand value)
        {
            _a01Context.Brands.Add(value);
            _a01Context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { brandId = value.BrandId }, value);
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{BrandId}")]
        public IActionResult Put(int BrandId, [FromBody] Brand value)
        {
            if (BrandId != value.BrandId)
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
                if (_a01Context.Brands.Any(u => u.BrandId == BrandId))
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

        // DELETE api/<BrandsController>/5
        [HttpPost("{BrandId}")]
        public IActionResult Delete(int BrandId)
        {
            var delete = _a01Context.Brands.Find(BrandId);
            if (delete == null)
            {
                return NotFound();
            }
            _a01Context.Brands.Remove(delete);
            _a01Context.SaveChanges();
            return NoContent();
        }*/
    }
}
