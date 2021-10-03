# Notes

```csharp
using var connection = new SqliteConnection("Filename=:memory:");
connection.Open();
var builder = new DbContextOptionsBuilder<ComicsContext>();
builder.UseSqlite(connection);
using var context = new ComicsContext(builder.Options);
context.Database.EnsureCreated();
```
