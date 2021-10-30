namespace Generics
{
    public interface ITrafficLightController
    {
        bool MayIGo(TrafficLightColor color);
    }

    public interface IBadTrafficLightController
    {
        bool MayIGo(string color);
    }
}
