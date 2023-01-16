using System.ComponentModel.DataAnnotations;

namespace BlogSitesiProjesi.ViewModels.Auth.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email bos olamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
