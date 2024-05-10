using System;
using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class Card
    {
        public int Id { get; set; }

        [Required]
        public string CardName { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string Priority { get; set; }

        [Required]
        public int ListId { get; set; }

        public List List { get; set; }
    }
}
