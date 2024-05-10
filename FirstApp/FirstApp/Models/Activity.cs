using System;
using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Type { get; set; } //1 - создание, 2 - удаление, 3 - переименование, 4 - перемещение карточки из одного списка в другой, 5 - смена приоритета

        [Required]
        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}
