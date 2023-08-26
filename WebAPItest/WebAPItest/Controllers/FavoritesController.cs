using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItest.Dtos;
using WebAPItest.Models;
using System.IdentityModel.Tokens.Jwt;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPItest.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly A01Context _a01Context;
        private readonly IConfiguration _configuration;


        public FavoritesController(IConfiguration configuration, A01Context a01Context)
        {
            _configuration = configuration;
            _a01Context = a01Context;

            // 初始化Firebase Admin SDK
            var firebaseCredentialsPath = "ar-home-design-firebase-adminsdk-pcy6j-cadf5e2d4d.json"; // 替換為您的Service Account金鑰的路徑
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(firebaseCredentialsPath)
            });
        }

        // GET: api/<FavoritesController>
        [HttpGet]
        public IEnumerable<FavoritesDto> Get()
        {
            var userId = GetUserIdFromFirebaseToken(); // 使用您的方法從Firebase ID令牌中提取用戶ID

            if (userId == null)
            {
                // 可以在這裡處理無效的用戶，例如回傳 401 Unauthorized
                return Enumerable.Empty<FavoritesDto>();
            }
            var result = (from b in _a01Context.Favorites
                          .Include(b => b.Furniture)
                          where b.UserId == userId
                          && b.FurnitureId != null // 確保 FurnitureId 不為 null
                          select new FavoritesDto
                          {
                              UserId = b.UserId,
                              FurnitureId = b.FurnitureId,
                              Type=b.Furniture.Type,
                              Color=b.Furniture.Color,
                              Style=b.Furniture.Style,
                              Brand1 = (b.Furniture.Brand==null) ? null : b.Furniture.Brand.Brand1,
                              PhoneNumber = (b.Furniture.Brand == null) ? null : b.Furniture.Brand.PhoneNumber,
                              Address= (b.Furniture.Brand == null) ? null : b.Furniture.Brand.Address,
                              Logo= (b.Furniture.Brand == null) ? null :  b.Furniture.Brand.Logo,
                              Location=b.Furniture.Location,
                              Picture=b.Furniture.Picture
                          }).ToList();
            return result;
        }
        // GET api/<FavoritesController>/5
        [HttpGet("{userId}")]
        public IEnumerable<FavoritesDto> Get(string userId)
        {
            var result = (from b in _a01Context.Favorites
                          .Include(b => b.Furniture)
                          where b.UserId == userId
                          select new FavoritesDto
                          {
                              UserId = b.UserId,
                              FurnitureId = b.FurnitureId,
                              Type=b.Furniture.Type,
                              Color=b.Furniture.Color,
                              Style=b.Furniture.Style,
                              Brand1 = (b.Furniture.Brand==null) ? null : b.Furniture.Brand.Brand1,
                              PhoneNumber = (b.Furniture.Brand == null) ? null : b.Furniture.Brand.PhoneNumber,
                              Address= (b.Furniture.Brand == null) ? null : b.Furniture.Brand.Address,
                              Logo= (b.Furniture.Brand == null) ? null : b.Furniture.Brand.Logo,
                              Location=b.Furniture.Location,
                              Picture=b.Furniture.Picture
                          }).ToList();
            return result;
        }

        // POST api/<FavoritesController>
        [HttpPost]
        public void Post([FromBody] FavoritesDto value)
        {
            var userId = GetUserIdFromFirebaseToken(); // 使用您的方法從Firebase ID令牌中提取用戶ID
            
            Favorite insert = new Favorite
            {
                UserId=userId,
                FurnitureId=value.FurnitureId
            };
            _a01Context.Favorites.Add(insert);
            _a01Context.SaveChanges();
        }

        // DELETE api/<FavoritesController>/5
        [HttpPost("{furnitureId}")]
        public IActionResult Delete(int furnitureId)
        {
            var userId = GetUserIdFromFirebaseToken(); // 使用您的方法從Firebase ID令牌中提取用戶ID

            if (userId == null)
            {
                // 可以在這裡處理無效的 nameId，例如回傳 401 Unauthorized
                return Unauthorized();
            }

            var delete = (from b in _a01Context.Favorites
                          where b.UserId == userId
                          && b.FurnitureId == furnitureId
                          && b.FurnitureId != null // 確保 FurnitureId 不為 null
                          select b).SingleOrDefault();

            if (delete == null)
            {
                return NotFound();
            }
            _a01Context.Favorites.Remove(delete);
            _a01Context.SaveChanges();
            return NoContent();
        }

        private string GetUserIdFromFirebaseToken()
        {
            // 在這裡實現Firebase身份驗證並提取用戶ID的邏輯
            // 這部分的程式碼將依賴於您的Firebase設置和庫的使用方式

            try
            {
                var idToken = HttpContext.Request.Headers["Authorization"].ToString();

                // 驗證 ID 令牌
                var decodedToken = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(idToken).Result;

                // 從解碼的令牌中獲取用戶ID
                var userId = decodedToken.Uid;

                return userId;
            }
            catch (Exception ex)
            {
                // 處理驗證失敗的情況
                // 這裡可以記錄錯誤或執行其他處理
                return null;
            }
        }

    }
}

