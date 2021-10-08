using System;
using System.Collections.Generic;

namespace Lecture05.Core
{
    public interface IPowerRepository
    {
        PowerDTO Create(CharacterCreateDTO power);
        PowerDTO Read(int powerId);
        IReadOnlyCollection<PowerDTO> Read();
        Response Update(PowerDTO power);
        Response Delete(int powerId);
    }
}
