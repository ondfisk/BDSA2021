// using System.Collections.Generic;
// using System.Data;
// using System.Data.SqlClient;
// using System.IO;

// namespace Lecture04
// {
//     public class RawSqlCharacterRepository
//     {
//         private readonly SqlConnection _connection;

//         public RawSqlCharacterRepository(SqlConnection connection)
//         {
//             _connection = connection;
//         }

//         public void Reset()
//         {
//             var cmdText = File.ReadAllText("FuturamaReset.sql");

//             using var command = new SqlCommand(cmdText, _connection);

//             OpenConnection();

//             command.ExecuteNonQuery();

//             CloseConnection();
//         }

//         public int Create(FuturamaCharacterDTO character)
//         {
//             var cmdText = @"INSERT Character (Name, Species, Planet, ActorId)
//                             VALUES (@Name, @Species, @Planet, @ActorId);
//                             SELECT SCOPE_IDENTITY()";

//             using var command = new SqlCommand(cmdText, _connection);

//             command.Parameters.AddWithValue("@Name", character.Name);
//             command.Parameters.AddWithValue("@Species", character.Species);
//             command.Parameters.AddWithValue("@Planet", character.Planet);
//             command.Parameters.AddWithValue("@ActorId", character.ActorId);

//             OpenConnection();

//             var id = command.ExecuteScalar();

//             CloseConnection();

//             return (int)id;
//         }

//         public FuturamaCharacterDTO Read(string name)
//         {
//             var cmdText = @"SELECT c.Id, c.Name, c.Species, c.Planet, c.ActorId, a.Name AS ActorName
//                             FROM Characters AS c
//                             LEFT JOIN Actors AS a ON c.ActorId = a.Id
//                             WHERE c.Name = @Name";

//             using var command = new SqlCommand(cmdText, _connection);

//             command.Parameters.AddWithValue("@Name", name);

//             OpenConnection();

//             using var reader = command.ExecuteReader();

//             var character = reader.Read()
//                 ? new FuturamaCharacterDTO
//                 {
//                     Id = reader.GetInt32("Id"),
//                     Name = reader.GetString("Name"),
//                     Species = reader.GetString("Species"),
//                     Planet = reader.GetString("Planet"),
//                     ActorId = reader.GetInt32("ActorId"),
//                     ActorName = reader.GetString("ActorName")
//                 }
//                 : null;

//             CloseConnection();

//             return character;
//         }

//         public IEnumerable<FuturamaCharacterDTO> Read()
//         {
//             var cmdText = @"SELECT c.Id, c.Name, c.Species, c.Planet, c.ActorId, a.Name AS ActorName
//                             FROM Characters AS c
//                             LEFT JOIN Actors AS a ON c.ActorId = a.Id
//                             ORDER BY c.Name";

//             using var command = new SqlCommand(cmdText, _connection);

//             OpenConnection();

//             using var reader = command.ExecuteReader();

//             while (reader.Read())
//             {
//                 yield return new FuturamaCharacterDTO
//                 {
//                     Id = reader.GetInt32("Id"),
//                     Name = reader.GetString("Name"),
//                     Species = reader.GetString("Species"),
//                     Planet = reader.GetString("Planet"),
//                     ActorId = reader.GetInt32("ActorId"),
//                     ActorName = reader.GetString("ActorName")
//                 };
//             }

//             CloseConnection();
//         }

//         public void Update(FuturamaCharacterDTO character)
//         {
//             var cmdText = @"UPDATE Characters SET
//                             Name = @Name,
//                             Species = @Species,
//                             Planet = @Planet,
//                             ActorId = @ActorId
//                             WHERE Id = @Id";

//             using var command = new SqlCommand(cmdText, _connection);

//             command.Parameters.AddWithValue("@Id", character.Id);
//             command.Parameters.AddWithValue("@Name", character.Name);
//             command.Parameters.AddWithValue("@Species", character.Species);
//             command.Parameters.AddWithValue("@Planet", character.Planet);
//             command.Parameters.AddWithValue("@ActorId", character.ActorId);

//             OpenConnection();

//             command.ExecuteNonQuery();

//             CloseConnection();
//         }

//         public void Delete(int characterId)
//         {
//             var cmdText = @"DELETE Characters WHERE Id = @Id";

//             using var command = new SqlCommand(cmdText, _connection);

//             command.Parameters.AddWithValue("@Id", characterId);

//             OpenConnection();

//             command.ExecuteNonQuery();

//             CloseConnection();
//         }

//         private void OpenConnection()
//         {
//             if (_connection.State == ConnectionState.Closed)
//             {
//                 _connection.Open();
//             }
//         }

//         private void CloseConnection()
//         {
//             if (_connection.State == ConnectionState.Open)
//             {
//                 _connection.Close();
//             }
//         }

//         public void Dispose()
//         {
//             _connection.Dispose();
//         }
//     }
// }