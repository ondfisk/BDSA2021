using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lecture04.Core;

namespace Lecture04.Entities
{
    public class Character
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string GivenName { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string AlterEgo { get; set; }

        public DateTime FirstAppearance { get; set; }

        [StringLength(50)]
        public string Occupation { get; set; }

        public int? CityId { get; set; }

        public City City { get; set; }

        public Gender Gender { get; set; }

        public ICollection<Power> Powers { get; set; }
    }
}
