using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FirstApp.Models
{
    public class List
    {
        public int Id { get; set; }

        [Required]
        public string ListName { get; set; }

        public ICollection<Card> Cards { get; set; }
    }
}
