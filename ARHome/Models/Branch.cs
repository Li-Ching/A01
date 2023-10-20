using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ARHome.Models
{
    public class Branch
    {        
        public int branchId { get; set; }              // 用於儲存流水號
        
        [Display(Name = "品牌")]
        public string? brand1 { get; set; }              // 用於儲存流水號

        [Display(Name = "分店")]
        public string? branchName { get; set; }              // 用於儲存流水號

        [Display(Name = "分店電話")]
        public string? phoneNumber { get; set; }              // 用於儲存流水號

        [Display(Name = "分店地址")]
        public string? address { get; set; }              // 用於儲存流水號
        


    }
}
