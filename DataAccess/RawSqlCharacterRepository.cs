namespace DataAccess;

public class RawSqlCharacterRepository
{
    private readonly SqlConnection _connection;

    public RawSqlCharacterRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public int Create(CharacterDTO character)
    {
        var cmdText = @"INSERT Character (Name, Species, Planet, ActorId)
                            VALUES (@Name, @Species, @Planet, @ActorId);
                            SELECT SCOPE_IDENTITY()";

        using var command = new SqlCommand(cmdText, _connection);

        command.Parameters.AddWithValue("@Name", character.Name);
        command.Parameters.AddWithValue("@Species", character.Species);
        command.Parameters.AddWithValue("@Planet", character.Planet);
        command.Parameters.AddWithValue("@ActorId", character.ActorId);

        OpenConnection();

        var id = command.ExecuteScalar();

        CloseConnection();

        return (int)id;
    }

    public CharacterDTO? Read(string name)
    {
        var cmdText = @"SELECT c.Id, c.Name, c.Species, c.Planet, c.ActorId, a.Name AS ActorName
                            FROM Characters AS c
                            LEFT JOIN Actors AS a ON c.ActorId = a.Id
                            WHERE c.Name = @Name";

        using var command = new SqlCommand(cmdText, _connection);

        command.Parameters.AddWithValue("@Name", name);

        OpenConnection();

        using var reader = command.ExecuteReader();

        var character = reader.Read()
            ? new CharacterDTO(
                reader.GetInt32("Id"),
                reader.GetString("Name"),
                reader.GetString("Species"),
                reader.GetString("Planet"),
                reader.GetInt32("ActorId"),
                reader.GetString("ActorName")
            )
            : null;

        CloseConnection();

        return character;
    }

    public IEnumerable<CharacterDTO> Read()
    {
        var cmdText = @"SELECT c.Id, c.Name, c.Species, c.Planet, c.ActorId, a.Name AS ActorName
                            FROM Characters AS c
                            LEFT JOIN Actors AS a ON c.ActorId = a.Id
                            ORDER BY c.Name";

        using var command = new SqlCommand(cmdText, _connection);

        OpenConnection();

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new CharacterDTO(
                reader.GetInt32("Id"),
                reader.GetString("Name"),
                reader.GetString("Species"),
                reader.GetString("Planet"),
                reader.GetInt32("ActorId"),
                reader.GetString("ActorName")
            );
        }

        CloseConnection();
    }

    public void Update(CharacterDTO character)
    {
        var cmdText = @"UPDATE Characters SET
                            Name = @Name,
                            Species = @Species,
                            Planet = @Planet,
                            ActorId = @ActorId
                            WHERE Id = @Id";

        using var command = new SqlCommand(cmdText, _connection);

        command.Parameters.AddWithValue("@Id", character.Id);
        command.Parameters.AddWithValue("@Name", character.Name);
        command.Parameters.AddWithValue("@Species", character.Species);
        command.Parameters.AddWithValue("@Planet", character.Planet);
        command.Parameters.AddWithValue("@ActorId", character.ActorId);

        OpenConnection();

        command.ExecuteNonQuery();

        CloseConnection();
    }

    public void Delete(int characterId)
    {
        var cmdText = @"DELETE Characters WHERE Id = @Id";

        using var command = new SqlCommand(cmdText, _connection);

        command.Parameters.AddWithValue("@Id", characterId);

        OpenConnection();

        command.ExecuteNonQuery();

        CloseConnection();
    }

    private void OpenConnection()
    {
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }
    }

    private void CloseConnection()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _connection.Close();
        }
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
