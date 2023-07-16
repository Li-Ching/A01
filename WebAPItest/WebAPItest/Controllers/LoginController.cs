using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPItest.Dtos;
using WebAPItest.Models;

namespace WebAPItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly A01Context _a01Context;

        public LoginController(A01Context a01Context)
        {
            _a01Context = a01Context;
        }

        [HttpPost]
        public IActionResult login(LoginPost value)
        {
            var user = (from a in _a01Context.Users
                        where a.Email == value.Email
                        && a.Password == value.Password
                        select a).SingleOrDefault();

            if (user == null)
            {
                return BadRequest(new { status = "帳號密碼錯誤" });
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, String.IsNullOrEmpty(user.Email)?"":user.Email),
                    new Claim("FullName", String.IsNullOrEmpty(user.Username)?"": user.Username),
                    new Claim("UserId", user.UserId.ToString())
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Ok(new { status = "登入成功" });
            }
        }

        [HttpPost("logout")]
        public void logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        [HttpGet("NoLogin")]
        public IActionResult noLogin()
        {
            return Ok(new { status = "未登入" });
        }

    }
}
