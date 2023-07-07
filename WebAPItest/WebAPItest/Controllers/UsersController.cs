using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItest.Dtos;
using WebAPItest.Models;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly A01Context _a01Context;

        public UsersController(A01Context a01Context) {
            _a01Context = a01Context;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<UsersDto> Get()
        {
            var result = _a01Context.Users
                .Select(a => a);

            return result.ToList().Select(a=>ItemToDto(a));
        }

        // GET api/<UsersController>/5
        [HttpGet("{UserId}")]
        public IActionResult Get(int UserId)
        {
            var result = _a01Context.Users.Find(UserId);
            if (result == null)
            {
                return NotFound("沒有此使用者");
            }
            return Ok(result);
        }



        // POST api/<UsersController>
        [HttpPost]
        public ActionResult<User> Post([FromBody] User value)
        {
            _a01Context.Users.Add(value);
            _a01Context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId = value.UserId }, value);
        }



        // DELETE api/<UsersController>/5
        [Authorize]
        [HttpPost("{UserId}")]
        public IActionResult Delete(int UserId)
        {
            var delete = _a01Context.Users.Find(UserId);
            if (delete == null)
            {
                return NotFound();
            }
            _a01Context.Users.Remove(delete);
            _a01Context.SaveChanges();
            return NoContent();
        }

        private static UsersDto ItemToDto(User a)
        {
            return new UsersDto
            {
                UserId=a.UserId,
                Username=a.Username,
                Email=a.Email,
                Password=a.Password,
            };
        }


        // PUT api/<UsersController>/5
        [HttpPost]
        [Authorize]
        [Route("Update")]
        public IActionResult Put([FromBody] UsersUpdateDto value)
        {
            var Claim = HttpContext.User.Claims.ToList();
            var UserId = Claim.Where(a => a.Type == "UserId").First().Value;

            var update = _a01Context.Users.Find(Int32.Parse(UserId));

            if (update==null)
            {
                return BadRequest();
            }

            update.Username= value.Username;
            update.Password= value.Password;

            try
            {
                _a01Context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (_a01Context.Users.Any(u => u.UserId == Int32.Parse(UserId)))
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
    }
}
