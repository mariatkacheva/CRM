using System.ComponentModel.DataAnnotations;

namespace LDanceCRMRazorPages3.ViewModel
{
    public class Login
    {
        [Required(ErrorMessage = "Поле номера телефона обязательно для заполнения.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле пароля обязательно для заполнения.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
