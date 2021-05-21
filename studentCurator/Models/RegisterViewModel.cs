using System.ComponentModel.DataAnnotations;

namespace studentCurator.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string SurName { get; set; }
        [Required]
        [Display(Name = "Отчество")]
        public string Father { get; set; }
        [Required]
        [Display(Name = "Логин")]
        public string Email { get; set; }
 
        [Required]
        [Display(Name = "Кафедра")]
        public string Cathedra { get; set; }
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}