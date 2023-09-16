using Microsoft.AspNetCore.Mvc;
// JSON序列化與反序列化所需的名稱空間套件
using System.Text.Json;
using System.Text;
using ARHome.Models;

namespace ARHome.Controllers
{
    public class BrandsController : Controller
    {// 留言板服務在私有雲虛擬機執行時的網址 (要將此網址改為你的Web API網址)
        string baseurl = "http://192.168.1.104/A01API/api/Brands";

        // GET: MessageBoard
        public async Task<IActionResult> Index()
        {
            //建立HttClient物件
            using (HttpClient client = new HttpClient())
            {
                // 設定[取得全部留言紀錄]Web API之網址
                string url = baseurl;

                // 以非同步GET方式呼叫Web API
                var response = await client.GetAsync(url);
                // 以非同步方式取出API回傳之JSON格式字串
                string apiResponse = await response.Content.ReadAsStringAsync();
                // 將回傳的JSON格式字串反序列化成JSON物件
                var resultJsonObj = JsonSerializer.Deserialize<List<Brand>>(apiResponse);

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
                Brand resultJsonObj = JsonSerializer.Deserialize<Brand>(apiResponse);

                if (resultJsonObj == null)
                {
                    return NotFound();
                }

                // 將resultJsonObj物件傳給View頁面，並回傳View頁面
                return View(resultJsonObj);
            }
        }
    }
}