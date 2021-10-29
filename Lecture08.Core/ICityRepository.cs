using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecture08.Core
{
    public interface ICityRepository
    {
        Task<(Status, CityDTO)> CreateAsync(CityCreateDTO city);
        Task<CityDTO> ReadAsync(int cityId);
        Task<IReadOnlyCollection<CityDTO>> ReadAsync();

        Task<Status> UpdateAsync(CityDTO city);
        Task<Status> DeleteAsync(int cityId);
    }
}
