using System.ComponentModel.DataAnnotations;

namespace MyApp.Core
{
    public record CityCreateDto([Required, StringLength(50)] string Name);
    public record CityDto(int Id, [Required, StringLength(50)] string Name) : CityCreateDto(Name);
}
