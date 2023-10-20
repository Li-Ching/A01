using Microsoft.AspNetCore.Mvc;
// JSON序列化與反序列化所需的名稱空間套件
using System.Text.Json;
using System.Text;
using ARHome.Models;

namespace ARHome.Controllers
{
    public class BrandsController : Controller
    {// 留言板服務在私有雲虛擬機執行時的網址 (要將此網址改為你的Web API網址)
        string baseurl = "http://140.137.41.136:1380/A01/api/Brands";

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
                string url2 = "http://140.137.41.136:1380/A01/api/Branches/"+ id.ToString();

                var response = await client.GetAsync(url);
                string apiResponse = await response.Content.ReadAsStringAsync();
                Brand resultJsonObj = JsonSerializer.Deserialize<Brand>(apiResponse);

                var response2 = await client.GetAsync(url2);
                string apiResponse2 = await response2.Content.ReadAsStringAsync();
                List<Brand.Branch> messagesFromApiResponse2 = JsonSerializer.Deserialize<List<Brand.Branch>>(apiResponse2);


                if (resultJsonObj == null)
                {
                    return NotFound();
                }

                // 初始化 Messages 屬性
                if (resultJsonObj.Branches == null)
                {
                    resultJsonObj.Branches = new List<Brand.Branch>();
                }


                // 將新的留言加入到 resultJsonObj.Messages 中
                resultJsonObj.Branches.AddRange(messagesFromApiResponse2);
                resultJsonObj.Branches = resultJsonObj.Branches;

                // 將resultJsonObj物件傳給View頁面，並回傳View頁面
                return View(resultJsonObj);
            }
        }
    }
}