using System;
using System.ComponentModel.DataAnnotations;

namespace LDanceCRMRazorPages3.ViewModel
{
    public class Register
    {
        [Required(ErrorMessage = "Поле номера телефона обязательно для заполнения.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле пароля обязательно для заполнения.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле подтверждения пароля обязательно для заполнения.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }

        //добавленные поля
        [Required(ErrorMessage = "Поле фамилии обязательно для заполнения.")]
        [DataType(DataType.Text)]
        public string ClientSurname { get; set; }

        [Required(ErrorMessage = "Поле имени обязательно для заполнения.")]
        [DataType(DataType.Text)]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Поле отчества обязательно для заполнения.")]
        [DataType(DataType.Text)]
        public string ClientMiddleName { get; set; }

        [Required(ErrorMessage = "Поле дата рождения обязательно для заполнения.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата рождения")]
        public DateTime? ClientBirthDate { get; set; }

    }
}
