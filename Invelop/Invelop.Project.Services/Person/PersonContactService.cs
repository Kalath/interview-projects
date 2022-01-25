using Invelop.Project.Core.Models;
using Invelop.Project.Repository.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invelop.Project.Services.Person
{
    public class PersonContactService
    {
        public IEnumerable<PersonContacts> GetAll()
        {
            return new List<PersonContacts>();
        }

        public async Task<PersonContacts?> Get(int Id)
        {
            var personContactsRepository = new PersonContactsRepository();
            return await personContactsRepository.Get(Id);
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
