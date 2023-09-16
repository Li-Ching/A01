using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ARHome.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        
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
