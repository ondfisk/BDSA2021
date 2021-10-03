using System;
using System.Collections.Generic;

namespace Lecture05.Core
{
    public interface ICityRepository
    {
        CityDTO Create(CityCreateDTO city);
        CityDTO Read(int cityId);
        IReadOnlyCollection<CityDTO> Read();
        Response Update(CityDTO city);
        Response Delete(int cityId);
    }
}
