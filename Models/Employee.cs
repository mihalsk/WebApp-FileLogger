using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    /// <summary>
    /// Модель работника
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        [Display(Name = "ФИО")] //  Переопределяем выводимое имя для поля
        public string Name { get; set; }
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }
    }
}
