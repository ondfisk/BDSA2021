using System;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Core
{
    public record PowerCreateDto([Required, StringLength(50)] string Name);
    public record PowerDto(int Id, [Required, StringLength(50)] string Name) : PowerCreateDto(Name);
}
