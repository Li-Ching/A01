using Microsoft.AspNetCore.Mvc;
// JSON序列化與反序列化所需的名稱空間套件
using System.Text.Json;
using System.Text;
using ARHome.Models;

namespace ARHome.Controllers
{
    
    public class FavoritesController : Controller
    {// 留言板服務在私有雲虛擬機執行時的網址 (要將此網址改為你的Web API網址)
        string baseurl = "http://172.20.10.5/A01/api/Favorites";

        // GET: MessageBoard
        public async Task<IActionResult> Index()
        {
            // 確保用戶已登入 Firebase，並且已經獲得了 ID 令牌
            var token = HttpContext.Session.GetString("_UserToken");
            if (string.IsNullOrEmpty(token))
            {
                // 如果用戶未登入，您可以執行相應的操作，例如重定向到登入頁面
                return RedirectToAction("SignIn", "Home"); // 假設登入頁面是 "Account/Login"
            }

            // 建立 HttpClient 物件
            using (HttpClient client = new HttpClient())
            {
                // 設定 Web API 的網址，包括用戶的 UID
                var url = baseurl;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                // 以非同步 GET 方式呼叫 Web API
                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    // 以非同步方式取出 API 回應的 JSON 格式字串
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    // 將回傳的 JSON 格式字串反序列化成 Furniture 的集合（假設您的模型是 Furniture）
                    var resultJsonObj = JsonSerializer.Deserialize<List<Favorite>>(apiResponse);

                    // 將 resultJsonObj 物件傳遞給 View 頁面，並回傳 View 頁面
                    return View(resultJsonObj);
                }
                else
                {
                    // 處理 API 請求失敗的情況，例如顯示錯誤訊息
                    ViewBag.ErrorMessage = "無法檢索用戶的 Favorites。";
                    return View();
                }
            }
        }

        // POST: Favorites/ToggleFavorite
        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(int furnitureId)
        {
            // 从前端接收FurnitureId，并发送到Web API
            var token = HttpContext.Session.GetString("_UserToken");
            if (string.IsNullOrEmpty(token))
            {
                // 如果用戶未登入，您可以執行相應的操作，例如重定向到登入頁面
                return RedirectToAction("SignIn", "Home"); // 假設登入頁面是 "Account/Login"
            }

            // 建立 HttpClient 物件
            using (HttpClient client = new HttpClient())
            {
                // 設定 Web API 的網址
                var url = baseurl;

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // 建立匿名類別，包含FurnitureId
                var postData = new { FurnitureId = furnitureId };

                // 將要傳送的數據序列化為JSON格式
                var postDataJson = JsonSerializer.Serialize(postData);

                // 建立要傳送的內容
                var content = new StringContent(postDataJson, Encoding.UTF8, "application/json");

                // 以非同步POST方式呼叫Web API，將JSON數據傳送給API
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    // 處理成功的情況，例如重定向到成功頁面或其他操作
                    return RedirectToAction("Success");
                }
                else
                {
                    // 處理API請求失敗的情況，例如顯示錯誤訊息
                    ViewBag.ErrorMessage = "無法新增Favorite。";
                    return View();
                }
            }
        }

        }
}
