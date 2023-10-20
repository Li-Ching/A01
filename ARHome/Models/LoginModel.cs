using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ARHome.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "信箱")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "密碼")]
        [Required]
        public string Password { get; set; }
        
        [Display(Name = "暱稱")]
        [Required]
        public string displayName { get; set; }
    }

    public class FirebaseError
    {
        public Error error { get; set; }
    }


    public class Error
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Error> errors { get; set; }
    }
}
