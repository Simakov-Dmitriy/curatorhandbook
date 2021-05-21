using System.ComponentModel.DataAnnotations;

namespace studentCurator.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Укажите имя студента")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите фамилию студента")]
        public string SurName { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage = "Укажите возраст студента")]
        public int Age { get; set; }
        public string Faculty { get; set; }
        public int Course { get; set; }
        public int DaysGone { get; set; }
        public double Attendance { get; set; }
        public string AverageMark { get; set; }
    }
}