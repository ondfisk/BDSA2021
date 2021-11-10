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
}
