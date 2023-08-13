﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ARHome.Models
{
    public class Furniture
    {        
        public int furnitureId { get; set; }              // 用於儲存流水號
        
        [Display(Name = "家具類型")]
        public string? type { get; set; }          // 定義[家具類型]欄位

        [Display(Name = "家具顏色")]
        public string? color { get; set; } //  定義[家具顏色]欄位

        [Display(Name = "家具風格")]
        public string? style { get; set; }     // 定義[家具風格]欄位

        public string? brand1 { get; set; }              // 用於儲存流水號

        public string? phoneNumber { get; set; }              // 用於儲存流水號

        public string? address { get; set; }              // 用於儲存流水號
        [Display(Name = "圖片位址")]
        public string? picture { get; set; }     // 定義[圖片位址]欄位


    }
}
