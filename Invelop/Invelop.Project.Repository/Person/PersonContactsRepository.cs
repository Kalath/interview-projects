using Invelop.Project.Core.Models;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Invelop.Project.Repository.Person
{
    public class PersonContactsRepository : IPersonContactsRepository
    {
        private readonly string _connectionString;

        public PersonContactsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlLiteConnectionString");
        }

        public async Task<IEnumerable<PersonContacts>> GetAll()
        {
            using var sqlConnection = new SqliteConnection(_connectionString);
            await sqlConnection.OpenAsync();

            var personsContacts = new List<PersonContacts>();

            using var command = sqlConnection.CreateCommand();

            const string sqlQuery = "SELECT [Id], [Firstname], [Surname], [DateOfBirth], [Address], [PhoneNumber], [IBAN] FROM [PersonContacts];";
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                personsContacts.Add(new PersonContacts
                {
                    Id = reader.GetInt64(0),
                    Firstname = reader.GetString(1),
                    Surname = reader.GetString(2),
                    DateOfBirth = reader.IsDBNull(3) ? null : DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).Date,
                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                    PhoneNumber = reader.IsDBNull(5) ? null : reader.GetString(5),
                    IBAN = reader.IsDBNull(6) ? null : reader.GetString(6)
                });
            }

            return personsContacts;
        }

        public async Task<PersonContacts?> Get(long Id)
        {
            using var sqlConnection = new SqliteConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var command = sqlConnection.CreateCommand();

            const string sqlQuery = "SELECT [Id], [Firstname], [Surname], [DateOfBirth], [Address], [PhoneNumber], [IBAN] FROM [PersonContacts] WHERE [Id] = @Id;";
            command.CommandText = sqlQuery;
            command.CommandType = CommandType.Text;
            command.Parameters.AddWithValue(nameof(Id), Id);

            using var reader = await command.ExecuteReaderAsync();
            var exists = await reader.ReadAsync();

            if (exists)
            {
                return new PersonContacts
                {
                    Id = reader.GetInt64(0),
                    Firstname = reader.GetString(1),
                    Surname = reader.GetString(2),
                    DateOfBirth = reader.IsDBNull(3) ? null : DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).Date,
                    Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                    PhoneNumber = reader.IsDBNull(5) ? null : reader.GetString(5),
                    IBAN = reader.IsDBNull(6) ? null : reader.GetString(6)
                };

            }

            return default;
        }

        public async Task<long> Insert(PersonContacts personContacts)
        {
            using var sqlConnection = new SqliteConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var command = sqlConnection.CreateCommand();

            const string sqlInsertQuery = "INSERT INTO [PersonContacts] ([Firstname],[Surname],[DateOfBirth],[Address],[PhoneNumber],[IBAN]) VALUES(@Firstname,@Surname,@DateOfBirth,@Address,@PhoneNumber,@IBAN);";
            command.CommandText = sqlInsertQuery;
            command.CommandType = CommandType.Text;

            long? dateOfBirthEpoch = personContacts.DateOfBirth.HasValue ? new DateTimeOffset(personContacts.DateOfBirth.Value).ToUnixTimeSeconds() : null;
            command.Parameters.AddWithValue(nameof(personContacts.Firstname), personContacts.Firstname);
            command.Parameters.AddWithValue(nameof(personContacts.Surname), personContacts.Surname);
            command.Parameters.AddWithValue(nameof(personContacts.DateOfBirth), dateOfBirthEpoch.HasValue ? dateOfBirthEpoch : DBNull.Value);
            command.Parameters.AddWithValue(nameof(personContacts.Address), string.IsNullOrWhiteSpace(personContacts.Address) ? DBNull.Value : personContacts.Address);
            command.Parameters.AddWithValue(nameof(personContacts.PhoneNumber), string.IsNullOrWhiteSpace(personContacts.PhoneNumber) ? DBNull.Value : personContacts.PhoneNumber);
            command.Parameters.AddWithValue(nameof(personContacts.IBAN), string.IsNullOrWhiteSpace(personContacts.IBAN) ? DBNull.Value : personContacts.IBAN);

            var reader = await command.ExecuteNonQueryAsync();

            if (reader > 0)
            {
                const string lastIdQuery = "select last_insert_rowid();";
                command.CommandText = lastIdQuery;
                command.CommandType = CommandType.Text;

                using var lastRowReader = await command.ExecuteReaderAsync();
                var existsRowId = await lastRowReader.ReadAsync();

                return existsRowId && await lastRowReader.IsDBNullAsync(0) ? default : lastRowReader.GetInt64(0);
            }

            return default;
        }

        public async Task<bool> Update(PersonContacts personContacts)
        {
            using var sqlConnection = new SqliteConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var command = sqlConnection.CreateCommand();

            const string sqlUpdateQuery = "UPDATE [PersonContacts] SET [Firstname] = @Firstname,[Surname] = @Surname,[DateOfBirth] = @DateOfBirth,[Address] = @Address,[PhoneNumber] = @PhoneNumber,[IBAN] = @IBAN WHERE [Id] = @Id;";
            command.CommandText = sqlUpdateQuery;
            command.CommandType = CommandType.Text;

            long? dateOfBirthEpoch = personContacts.DateOfBirth.HasValue ? new DateTimeOffset(personContacts.DateOfBirth.Value).ToUnixTimeSeconds() : null;
            command.Parameters.AddWithValue(nameof(personContacts.Id), personContacts.Id);
            command.Parameters.AddWithValue(nameof(personContacts.Firstname), personContacts.Firstname);
            command.Parameters.AddWithValue(nameof(personContacts.Surname), personContacts.Surname);
            command.Parameters.AddWithValue(nameof(personContacts.DateOfBirth), dateOfBirthEpoch.HasValue ? dateOfBirthEpoch : DBNull.Value);
            command.Parameters.AddWithValue(nameof(personContacts.Address), string.IsNullOrWhiteSpace(personContacts.Address) ? DBNull.Value : personContacts.Address);
            command.Parameters.AddWithValue(nameof(personContacts.PhoneNumber), string.IsNullOrWhiteSpace(personContacts.PhoneNumber) ? DBNull.Value : personContacts.PhoneNumber);
            command.Parameters.AddWithValue(nameof(personContacts.IBAN), string.IsNullOrWhiteSpace(personContacts.IBAN) ? DBNull.Value : personContacts.IBAN);

            var reader = await command.ExecuteNonQueryAsync();
            return reader > 0;
        }

        public async Task<bool> Delete(long Id)
        {
            using var sqlConnection = new SqliteConnection(_connectionString);
            await sqlConnection.OpenAsync();

            using var command = sqlConnection.CreateCommand();

            const string sqlDeleteQuery = "DELETE FROM [PersonContacts] WHERE [Id] = @Id;";
            command.CommandText = sqlDeleteQuery;
            command.CommandType = CommandType.Text;

            command.Parameters.AddWithValue(nameof(Id), Id);

            var reader = await command.ExecuteNonQueryAsync();
            return reader > 0;
        }
    }
}
