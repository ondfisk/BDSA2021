namespace DataAccess;

class Program
{
    static void Main(string[] args)
    {
        // var configuration = LoadConfiguration();
        // var connectionString = configuration.GetConnectionString("Futurama");

        // Console.Write("Input name: ");
        // var name = Console.ReadLine();
        // var cmdText = "SELECT * FROM Characters WHERE Name LIKE '%' + @name + '%'";

        // using var connection = new SqlConnection(connectionString);
        // using var command = new SqlCommand(cmdText, connection);

        // command.Parameters.AddWithValue("@name", name);

        // connection.Open();

        // using var reader = command.ExecuteReader();

        // while (reader.Read())
        // {
        //     var character = new
        //     {
        //         Name = reader.GetString("Name"),
        //         Species = reader.GetString("Species")
        //     };

        //     Console.WriteLine(character);
        // }

        var configuration = LoadConfiguration();
        var connectionString = configuration.GetConnectionString("Futurama");

        var optionsBuilder = new DbContextOptionsBuilder<FuturamaContext>().UseSqlServer(connectionString);
        using var context = new FuturamaContext(optionsBuilder.Options);
    }

    static IConfiguration LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>();

        return builder.Build();
    }
}
