namespace Generics.Tests;

using static TrafficLightColor;

public class TrafficLightControllerTests
{
    [Theory]
    [InlineData("Green", true)]
    [InlineData("GREEN", true)]
    [InlineData("Yellow", false)]
    [InlineData("yellow", false)]
    [InlineData("Red", false)]
    [InlineData("rEd", false)]
    public void MayIGo_given_string_color_returns_expected(string color, bool expected)
    {
        var ctrl = new TrafficLightController();

        var actual = ctrl.MayIGo(color);

        Assert.Equal(actual, expected);
    }

    [Fact]
    public void MayIGo_given_invalid_color_throws()
    {
        var ctrl = new TrafficLightController();

        var actual = Assert.Throws<ArgumentException>(() => ctrl.MayIGo("Blue"));

        Assert.Equal("Invalid color (Parameter 'color')", actual.Message);
    }

    [Theory]
    [InlineData(Green, true)]
    [InlineData(Yellow, false)]
    [InlineData(Red, false)]
    [InlineData((TrafficLightColor)42, false)]
    public void MayIGo_given_color_returns_expected(TrafficLightColor color, bool expected)
    {
        var ctrl = new TrafficLightController();

        var actual = ctrl.MayIGo(color);

        Assert.Equal(actual, expected);
    }
}
