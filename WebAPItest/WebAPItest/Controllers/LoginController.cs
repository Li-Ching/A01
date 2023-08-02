using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPItest.Dtos;
using WebAPItest.Models;

namespace WebAPItest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly A01Context _a01Context;
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration, A01Context a01Context)
        {
            _configuration = configuration;
            _a01Context = a01Context;
        }

        [HttpPost]
        public string login(LoginPost value)
        {
            var user = (from a in _a01Context.Users
                        where a.Email == value.Email
                        && a.Password == value.Password
                        select a).SingleOrDefault();

            if (user == null)
            {
                return "帳號密碼錯誤";
            }
            else
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, String.IsNullOrEmpty(user.Email)?"":user.Email),
                    new Claim("FullName", String.IsNullOrEmpty(user.Username)?"": user.Username),
                    new Claim("UserId", user.UserId.ToString())
                };

                //取出appsettings.json裡的KEY處理
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:KEY"]));

                //設定jwt相關資訊
                var jwt = new JwtSecurityToken
                (
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );


                //產生JWT Token
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                //回傳JWT Token給認證通過的使用者
                return token;
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
