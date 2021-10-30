using System;
using static Generics.TrafficLightColor;

namespace Generics
{
    public class TrafficLightController : ITrafficLightController, IBadTrafficLightController
    {
        public bool MayIGo(string color) => color.ToLowerInvariant() switch {
            "green" => true,
            "yellow" => false,
            "red" => false,
            _ => throw new ArgumentException("Invalid color", nameof(color))
        };

        public bool MayIGo(TrafficLightColor color) => color switch
        {
            Green => true,
            Yellow => false,
            Red => false,
            _ => false
        };
    }
}
