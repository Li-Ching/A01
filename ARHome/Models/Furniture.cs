using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ARHome.Models
{
    public class Furniture
    {        
        public int furnitureId { get; set; }              // 用於儲存流水號

        [Display(Name = "家具名稱")]
        public string? furnitureName { get; set; }          // 定義[家具名稱]欄位
        
        [Display(Name = "家具類型")]
        public string? type { get; set; }          // 定義[家具類型]欄位

        [Display(Name = "家具顏色")]
        public string? color { get; set; } //  定義[家具顏色]欄位

        [Display(Name = "家具風格")]
        public string? style { get; set; }     // 定義[家具風格]欄位

        [Display(Name = "品牌")]
        public string? brand1 { get; set; }              // 用於儲存流水號

        [Display(Name = "品牌電話")]
        public string? phoneNumber { get; set; }              // 用於儲存流水號

        [Display(Name = "品牌地址")]
        public string? address { get; set; }              // 用於儲存流水號

        [Display(Name = "圖片位址")]
        public string? picture { get; set; }     // 定義[圖片位址]欄位

        [Display(Name = "模型位址")]
        public string? location { get; set; }     // 定義[圖片位址]欄位

        public List<Message>? Messages { get; set; }

        public class Message
        {
            public Guid messageId { get; set; }
            public string? userId { get; set; }
            public string? message1 { get; set; }
            public DateTime messageTime { get; set; }
            public int furnitureId { get; set; }
        }
    }
}
