using System.ComponentModel.DataAnnotations;

namespace MyApp.Core
{
    public record CityCreateDTO([Required, StringLength(50)] string Name);
    public record CityDTO(int Id, [Required, StringLength(50)] string Name) : CityCreateDTO(Name);
}
