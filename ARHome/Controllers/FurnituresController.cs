using Microsoft.AspNetCore.Mvc;
// JSON序列化與反序列化所需的名稱空間套件
using System.Text.Json;
using System.Text;
using ARHome.Models;
using NuGet.Packaging;
using NuGet.Common;

namespace ARHome.Controllers
{
    public class FurnituresController : Controller
    {// 留言板服務在私有雲虛擬機執行時的網址 (要將此網址改為你的Web API網址)
        string baseurl = "http://140.137.41.136:1380/A01/api/Furnitures";
        string baseurl2 = "http://140.137.41.136:1380/A01/api/Messages";

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
                var resultJsonObj = JsonSerializer.Deserialize<List<Furniture>>(apiResponse);

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

            using (HttpClient client = new HttpClient())
            {
                string url = baseurl + "/" + id.ToString();
                string url2 = baseurl2 + "/"+ id.ToString();

                var response = await client.GetAsync(url);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Furniture resultJsonObj = JsonSerializer.Deserialize<Furniture>(apiResponse);

                var response2 = await client.GetAsync(url2);
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                List<Furniture.Message> messagesFromApiResponse2 = JsonSerializer.Deserialize<List<Furniture.Message>>(apiResponse2);

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
                resultJsonObj.Messages = resultJsonObj.Messages.OrderByDescending(m => m.messageTime).ToList();

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
                var resultJsonObj = JsonSerializer.Deserialize<List<Furniture>>(apiResponse);

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
                    string RequestJsonString = JsonSerializer.Serialize(Message);
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

    }
}
