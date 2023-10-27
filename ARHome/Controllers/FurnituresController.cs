using Microsoft.AspNetCore.Mvc;
// JSON序列化與反序列化所需的名稱空間套件
using System.Text.Json;
using System.Text;
using ARHome.Models;
using NuGet.Packaging;
using NuGet.Common;
using Firebase.Auth;
using Newtonsoft.Json;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace ARHome.Controllers
{
    public class FurnituresController : Controller
    {// 留言板服務在私有雲虛擬機執行時的網址 (要將此網址改為你的Web API網址)
        string baseurl = "http://140.137.41.136:1380/A01/api/Furnitures";
        string baseurl2 = "http://140.137.41.136:1380/A01/api/Messages";
        FirebaseAuthProvider auth;

        public FurnituresController()
        {
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyCuJICZfjPRYfSLZ4LRUqGris0T3klukKU"));

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

        // GET: MessageBoard
        public async Task<IActionResult> Index(string? type)
        {

            //建立HttClient物件
            using (HttpClient client = new HttpClient())
            {
                string url;
                ViewBag.type = "所有家具";
                if (type == null)
                {
                    // 設定[取得全部留言紀錄]Web API之網址
                    url = baseurl;
                }
                else
                {
                    // 設定[取得全部留言紀錄]Web API之網址
                    url = baseurl+"?Type="+type;
                    ViewBag.type = type;
                }

                // 以非同步GET方式呼叫Web API
                var response = await client.GetAsync(url);
                // 以非同步方式取出API回傳之JSON格式字串
                string apiResponse = await response.Content.ReadAsStringAsync();
                // 將回傳的JSON格式字串反序列化成JSON物件
                var resultJsonObj = System.Text.Json.JsonSerializer.Deserialize<List<Furniture>>(apiResponse);

                // 將resultJsonObj物件傳給View頁面，並回傳View頁面
                return View(resultJsonObj);
            }
        }

        // GET: MessageBoard/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var token = HttpContext.Session.GetString("_UserToken");
            // 使用 Firebase SDK 方法獲得目前登入使用者的唯一識別碼
            if (token != null)
            {
                var user = await auth.GetUserAsync(token);

                var decodedToken = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                    .VerifyIdTokenAsync(token).Result;

                // 從解碼的令牌中獲取用戶ID
                var userId = decodedToken.Uid;
                ViewBag.id = userId;
            }

            using (HttpClient client = new HttpClient())
            {
                string url = baseurl + "/" + id.ToString();
                string url2 = baseurl2 + "/"+ id.ToString();

                var response = await client.GetAsync(url);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Furniture resultJsonObj = System.Text.Json.JsonSerializer.Deserialize<Furniture>(apiResponse);

                var response2 = await client.GetAsync(url2);
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                List<Furniture.Message> messagesFromApiResponse2 = System.Text.Json.JsonSerializer.Deserialize<List<Furniture.Message>>(apiResponse2);

                // 檢查 resultJsonObj 是否為空
                if (resultJsonObj == null)
                {
                    return NotFound();
                }

                // 初始化 Messages 屬性
                if (resultJsonObj.Messages == null)
                {
                    resultJsonObj.Messages = new List<Furniture.Message>();
                }

                // 將新的留言加入到 resultJsonObj.Messages 中
                resultJsonObj.Messages.AddRange(messagesFromApiResponse2);

                foreach (var message in messagesFromApiResponse2)
                {
                    // 使用者 ID
                    var userId = message.userId;

                    // 使用 FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.GetUserAsync(userId) 來獲取使用者資訊
                    UserRecord userRecord = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.GetUserAsync(userId);

                    message.displayName = userRecord.DisplayName;
                }

                resultJsonObj.Messages = resultJsonObj.Messages.Where(n => n.isDelete == false).OrderByDescending(m => m.messageTime).ToList();

                return View(resultJsonObj);
            }
        }


        public async Task<IActionResult> Search(string? content)
        {
            
            //建立HttClient物件
            using (HttpClient client = new HttpClient())
            {
                string url;
                if (content == null)
                {
                    // 設定[取得全部留言紀錄]Web API之網址
                    return BadRequest();
                }
                else
                {
                    // 設定[取得全部留言紀錄]Web API之網址
                    url = baseurl+"/Search?content="+content;
                }
                ViewBag.content = content;

                // 以非同步GET方式呼叫Web API
                var response = await client.GetAsync(url);
                // 以非同步方式取出API回傳之JSON格式字串
                string apiResponse = await response.Content.ReadAsStringAsync();
                // 將回傳的JSON格式字串反序列化成JSON物件
                var resultJsonObj = System.Text.Json.JsonSerializer.Deserialize<List<Furniture>>(apiResponse);

                // 將resultJsonObj物件傳給View頁面，並回傳View頁面
                return View(resultJsonObj);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([Bind("furnitureId, message1")] Furniture.Message Message)
        {
            var token = HttpContext.Session.GetString("_UserToken");
            if (string.IsNullOrEmpty(token))
            {
                // 如果用戶未登入，您可以執行相應的操作，例如重定向到登入頁面
                return RedirectToAction("SignIn", "Home"); // 假設登入頁面是 "Account/Login"
            }

            if (ModelState.IsValid)
            {
                //建立HttClient物件
                using (HttpClient client = new HttpClient())
                {

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    // 設定[更新第id筆留言紀錄]Web API之網址
                    string url = baseurl2;
                    // 設定Request Body JSON格式字串
                    string RequestJsonString = System.Text.Json.JsonSerializer.Serialize(Message);
                    // 建立傳遞內容HttpContent物件
                    HttpContent httpContent = new StringContent(RequestJsonString, Encoding.UTF8, "application/json");
                    // 以非同步PUT呼叫Web API
                    var response = await client.PostAsync(url, httpContent);
                    // 以非同步方式取出API回傳之JSON格式字串
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // 重新導向Index() Action
                    return RedirectToAction("Details", new { id = Message.furnitureId });
                }
            }
            // 將messageRecord物件傳給View頁面，並回傳View頁面
            return View(Message);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMseeage(Guid messageId)
        {
            try
            {
                var token = HttpContext.Session.GetString("_UserToken");

                if (string.IsNullOrEmpty(token))
                {
                    return Json(new { success = false, message = "用戶未登入" });
                }

                using (HttpClient client = new HttpClient())
                {
                    var url = baseurl2 + "/Delete";

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    string RequestJsonString = System.Text.Json.JsonSerializer.Serialize(messageId);
                    // 建立傳遞內容HttpContent物件
                    HttpContent httpContent = new StringContent(RequestJsonString, Encoding.UTF8, "application/json");
                    // 以非同步PUT呼叫Web API
                    var response = await client.PostAsync(url, httpContent);


                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();

                        return Json(new { success = true, message = result.ToString() });
                    }
                    else
                    {
                        return Json(new { success = false, message = "無法刪除留言" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "發生錯誤：" + ex.Message });
            }
        }

    }
}
