using System.ComponentModel.DataAnnotations;

namespace BlogSitesiProjesi.ViewModels.Auth.Register
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email bos olamaz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
