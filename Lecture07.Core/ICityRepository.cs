using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lecture07.Core
{
    public interface ICityRepository
    {
        Task<(Response, CityDTO)> CreateAsync(CityCreateDTO city);
        Task<CityDTO> ReadAsync(int cityId);
        Task<IReadOnlyCollection<CityDTO>> ReadAsync();

        Task<Response> UpdateAsync(CityDTO city);
        Task<Response> DeleteAsync(int cityId);
    }
}
