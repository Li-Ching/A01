using Microsoft.AspNetCore.Mvc;
// JSON序列化與反序列化所需的名稱空間套件
using System.Text.Json;
using System.Text;
using ARHome.Models;

namespace ARHome.Controllers
{
    public class FurnituresController : Controller
    {// 留言板服務在私有雲虛擬機執行時的網址 (要將此網址改為你的Web API網址)
        string baseurl = "http://192.168.1.104/A01API/api/Furnitures";

        // GET: MessageBoard
        public async Task<IActionResult> Index(string? type)
        {

            //建立HttClient物件
            using (HttpClient client = new HttpClient())
            {
                string url;
                if (type == null)
                {
                    // 設定[取得全部留言紀錄]Web API之網址
                    url = baseurl;
                }
                else
                {
                    // 設定[取得全部留言紀錄]Web API之網址
                    url = baseurl+"?Type="+type;
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

            //建立HttClient物件
            using (HttpClient client = new HttpClient())
            {
                // 設定[取得第id筆留言紀錄]Web API的網址
                string url = baseurl + "/" + id.ToString();

                // 以非同步GET方式呼叫Web API
                var response = await client.GetAsync(url);
                // 以非同步方式取出API回傳之JSON格式字串
                string apiResponse = await response.Content.ReadAsStringAsync();
                // 將回傳的JSON格式字串反序列化成JSON物件
                Furniture resultJsonObj = JsonSerializer.Deserialize<Furniture>(apiResponse);

                if (resultJsonObj == null)
                {
                    return NotFound();
                }

                // 將resultJsonObj物件傳給View頁面，並回傳View頁面
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

    }
}
