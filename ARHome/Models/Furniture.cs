using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ARHome.Models
{
    public class Furniture
    {        
        public int FurnitureId { get; set; }              // 用於儲存流水號
        
        [Display(Name = "家具類型")]
        public string? Type { get; set; }          // 定義[家具類型]欄位

        [Display(Name = "家具顏色")]
        public string? Color { get; set; } //  定義[家具顏色]欄位

        [Display(Name = "家具風格")]
        public string? Style { get; set; }     // 定義[家具風格]欄位

        public int BrandId { get; set; }              // 用於儲存流水號

        [Display(Name = "圖片位址")]
        public string? Picture { get; set; }     // 定義[圖片位址]欄位


    }
}
