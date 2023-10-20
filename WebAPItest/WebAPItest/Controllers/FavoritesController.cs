using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPItest.Dtos;
using WebAPItest.Models;
using System.IdentityModel.Tokens.Jwt;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using YamlDotNet.Core.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPItest.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly A01Context _a01Context;


        public FavoritesController(A01Context a01Context)
        {
            _a01Context = a01Context;
            // 檢查是否已經存在名為"default"的Firebase應用程序
            if (FirebaseApp.DefaultInstance == null)
            {
                // 如果不存在，則初始化FirebaseAdmin
                var firebaseCredentialsPath = "ar-home-design-firebase-adminsdk-pcy6j-cadf5e2d4d.json"; // 替換為您的Service Account金鑰的路徑
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(firebaseCredentialsPath)
                });
            }
        }

        // GET: api/<FavoritesController>
        [HttpGet]
        public IEnumerable<FavoritesDto> Get()
        {
            // 使用HttpContext清除緩存
            HttpContext.Items["CachedUserId"] = null;

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
                              FurnitureName = b.Furniture.FurnitureName,
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
        public class OperationResult
        {
            public string? Message { get; set; }
        }

        // GET api/<FavoritesController>/5
        [HttpGet("{furnitureId}")]
        public IActionResult Get(int furnitureId)
        {
            try
            {
                HttpContext.Items["CachedUserId"] = null;

                var userId = GetUserIdFromFirebaseToken();
                OperationResult result = new OperationResult();

                var existingFavorite = _a01Context.Favorites
                    .SingleOrDefault(f => f.UserId == userId && f.FurnitureId == furnitureId);

                if (existingFavorite != null)
                {
                    _a01Context.Favorites.Remove(existingFavorite);
                    _a01Context.SaveChanges();
                    result.Message = "刪除家具";
                }
                else
                {
                    Favorite insert = new Favorite
                    {
                        UserId = userId,
                        FurnitureId = furnitureId
                    };
                    _a01Context.Favorites.Add(insert);
                    _a01Context.SaveChanges();
                    result.Message = "新增家具";
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // 處理例外情況，例如記錄錯誤或返回適當的錯誤響應
                return StatusCode(500, new { Message = ex.Message });
            }
        }






        //// POST api/<FavoritesController>
        //[HttpPost]
        //public void Post([FromBody] Favorite value)
        //{

        //}

        //// DELETE api/<FavoritesController>/5
        //[HttpPost("{furnitureId}")]
        //public IActionResult Delete(int furnitureId)
        //{
        //    var userId = GetUserIdFromFirebaseToken(); // 使用您的方法從Firebase ID令牌中提取用戶ID

        //    if (userId == null)
        //    {
        //        // 可以在這裡處理無效的 nameId，例如回傳 401 Unauthorized
        //        return Unauthorized();
        //    }

        //    var delete = (from b in _a01Context.Favorites
        //                  where b.UserId == userId
        //                  && b.FurnitureId == furnitureId
        //                  select b).SingleOrDefault();

        //    if (delete == null)
        //    {
        //        return NotFound();
        //    }
        //    _a01Context.Favorites.Remove(delete);
        //    _a01Context.SaveChanges();
        //    return NoContent();
        //}
        private string GetUserIdFromFirebaseToken()
        {
            try
            {
                // 從 HTTP 請求標頭中獲取帶有 "Bearer " 前綴的令牌
                var authHeader = HttpContext.Request.Headers["Authorization"].ToString();

                // 檢查是否包含 "Bearer " 前綴
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    // 提取令牌部分，去除 "Bearer " 前綴
                    var idToken = authHeader.Substring("Bearer ".Length);

                    // 驗證 ID 令牌
                    var decodedToken = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                        .VerifyIdTokenAsync(idToken).Result;

                    // 從解碼的令牌中獲取用戶ID
                    var userId = decodedToken.Uid;

                    return userId;
                }
                else
                {
                    // 如果標頭不包含有效的 "Bearer " 令牌，記錄錯誤訊息
                    var errorMessage = "Authorization header does not contain a valid Bearer token.";
                    // 在這裡可以記錄錯誤信息，例如使用日誌庫
                    // 記錄到日誌中，以便稍後檢查
                    Console.WriteLine(errorMessage);

                    return null;
                }
            }
            catch (Exception ex)
            {
                // 處理驗證失敗的情況，記錄錯誤訊息
                var errorMessage = "An error occurred while verifying the Firebase token: " + ex.Message;
                // 在這裡可以記錄錯誤信息，例如使用日誌庫
                // 記錄到日誌中，以便稍後檢查
                Console.WriteLine(errorMessage);

                return null;
            }
        }

    }
}
