using System.ComponentModel.DataAnnotations;

namespace studentCurator.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Укажите факультет группы")]
        public string Faculty { get; set; }
        [Required(ErrorMessage = "Укажите курс")]
        public int Course { get; set; }
        [Required(ErrorMessage = "Укажите номер группы")]
        public string GroupNumber { get; set; }
    }
}