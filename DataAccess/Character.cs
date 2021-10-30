using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class Character
    {
        public int Id { get; set; }

        public int? ActorId { get; set; }

        public Actor Actor { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Species { get; set; }

        [StringLength(50)]
        public string Planet { get; set; }
    }
}
