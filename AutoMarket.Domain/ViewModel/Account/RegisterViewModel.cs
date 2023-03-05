using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Domain.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно быть в длину не более 20-ти символов")]
        [MinLength(3, ErrorMessage = "Имя должно быть в длину не менее 3-х символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "Пароль должен быть в длину не менее 6-ти символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Подтвердите Ваш пароль")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
    }
}
