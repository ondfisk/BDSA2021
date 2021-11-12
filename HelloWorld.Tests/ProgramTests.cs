namespace HelloWorld.Tests;

public class ProgramTests
{
    [Fact]
    public void Main_prints_Hello_World()
    {
        // Arrange
        var writer = new StringWriter();
        Console.SetOut(writer);

        // Act
        var program = Assembly.LoadFrom("HelloWorld.dll");
        program?.EntryPoint?.Invoke(null, new[] { Array.Empty<string>() });

        var output = writer.GetStringBuilder().ToString().Trim();

        // Assert
        Assert.Equal("Hello, World!", output);
    }

    [Fact]
    public void Main_given_Class_prints_Hello_Class()
    {
        // Arrange
        var writer = new StringWriter();
        Console.SetOut(writer);

        // Act
        var program = Assembly.LoadFrom("HelloWorld.dll");
        program?.EntryPoint?.Invoke(null, new[] { new[] { "Class" } });

        var output = writer.GetStringBuilder().ToString().Trim();

        // Assert
        Assert.Equal("Hello, Class!", output);
    }
}
