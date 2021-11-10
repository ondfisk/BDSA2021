namespace MyApp.Core;

public interface ICityRepository
{
    Task<(Status, CityDto)> CreateAsync(CityCreateDto city);
    Task<Option<CityDto>> ReadAsync(int cityId);
    Task<IReadOnlyCollection<CityDto>> ReadAsync();
    Task<Status> UpdateAsync(CityDto city);
    Task<Status> DeleteAsync(int cityId);
}
