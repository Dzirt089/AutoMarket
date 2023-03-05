using System.ComponentModel.DataAnnotations;

namespace AutoMarket.Domain.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20-ти символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину более 3-х символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
