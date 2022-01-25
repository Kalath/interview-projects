using Invelop.Project.Core.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invelop.Project.Repository.Person
{
    public class PersonContactsRepository
    {
        public IEnumerable<PersonContacts> GetAll()
        {
            return new List<PersonContacts>();
        }

        public async Task<PersonContacts?> Get(int Id)
        {
            using (var sqlConnection = new SqliteConnection(@"Data Source = ClientData\InvelopProject.db;Cache=Shared"))
            {
                await sqlConnection.OpenAsync();

                using var command = sqlConnection.CreateCommand();
                
                command.CommandText = "SELECT [Id], [Firstname], [Surname], [DateOfBirth], [Address], [PhoneNumber], [IBAN] FROM [PersonContacts] WHERE [Id] = @Id";
                command.CommandType = CommandType.Text;
                command.Parameters.AddWithValue(nameof(Id), Id);

                using var reader = await command.ExecuteReaderAsync();
                var exists = await reader.ReadAsync();

                if (exists)
                {
                    return new PersonContacts
                    {
                        Id = reader.GetInt32(0),
                        Firstname = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Dob = reader.IsDBNull(3) ? null : DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64(3)).Date,
                        Address = reader.GetString(4),
                        PhoneNumber = reader.GetString(5),
                        IBAN = reader.GetString(6)
                    };
                }
            }

            return default;
        }

        public int Insert(PersonContacts personContacts)
        {
            return 1;
        }

        public void Update(PersonContacts personContacts)
        {

        }

        public void Delete(int Id)
        {

        }
    }
}
