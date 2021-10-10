using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lecture04.Infrastructure
{
    public class Power
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
