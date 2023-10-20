using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebAPItest.Dtos;
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

        // POST api/<MessagesController>
        [HttpPost]
        public IActionResult Post([FromBody] MessagesDto value)
        {
            HttpContext.Items["CachedUserId"] = null;

            var userId = GetUserIdFromFirebaseToken();

            Models.Message insert = new Models.Message
            {
                MessageId = Guid.NewGuid(),
                UserId = userId,
                FurnitureId = value.FurnitureId,
                Message1 = value.Message1,
                MessageTime = DateTime.Now,
                IsDelete = false,
            };

            _a01Context.Messages.Add(insert);
            _a01Context.SaveChanges();
            return  Ok("新增留言成功"); ;
        }

        // POST api/<MessagesController>
        [HttpPost("Delete")]
        public IActionResult Delete([FromBody] string MessageId)
        {
            if (Guid.TryParse(MessageId, out Guid messageId))
            {
                var message = _a01Context.Messages.FirstOrDefault(n => n.MessageId == messageId);
                if (message == null)
                {
                    return BadRequest("刪除留言失敗");
                }
                message.IsDelete = true;

                _a01Context.SaveChanges();
                return Ok("刪除留言成功");
            }
            else
            {
                return BadRequest("無效的 MessageId 格式");
            }
        }

        /*// PUT api/<BrandsController>/5
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
