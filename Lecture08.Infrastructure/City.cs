using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lecture08.Infrastructure
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
