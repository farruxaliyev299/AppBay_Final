using System.ComponentModel.DataAnnotations;

namespace AppBayBack.ViewModels.Account
{
    public class RegisterVM
    {
        public string Username { get; set; }
        [Required , DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password) , Compare("Password")]
        public string CheckPassword { get; set; }
    }
}
