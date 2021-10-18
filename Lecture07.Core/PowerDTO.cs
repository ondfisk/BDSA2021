using System;
using System.ComponentModel.DataAnnotations;

namespace Lecture07.Core
{
    public record PowerCreateDTO([Required, StringLength(50)] string Name);
    public record PowerDTO(int Id, [Required, StringLength(50)] string Name) : PowerCreateDTO(Name);
}
