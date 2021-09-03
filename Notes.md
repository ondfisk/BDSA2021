# Notes

## Hello World

```csharp
// Arrange
var writer = new StringWriter();
Console.SetOut(writer);

// Act
Program.Main(new string[0]);
var output = writer.GetStringBuilder().ToString().Trim();

// Assert
Assert.Equal("Hello, World!", output);
```
