using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MedTwinMVC.Models.DatabaseModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин!")]
        [DisplayName("Логин")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
