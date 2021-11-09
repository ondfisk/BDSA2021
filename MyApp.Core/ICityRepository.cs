using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Core
{
    public interface ICityRepository
    {
        Task<(Status, CityDto)> CreateAsync(CityCreateDto city);
        Task<CityDto> ReadAsync(int cityId);
        Task<IReadOnlyCollection<CityDto>> ReadAsync();

        Task<Status> UpdateAsync(CityDto city);
        Task<Status> DeleteAsync(int cityId);
    }
}
