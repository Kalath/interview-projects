using Invelop.Project.Core.Models;
using Invelop.Project.Repository.Person;

namespace Invelop.Project.Services.Person
{
    public class PersonContactsService : IPersonContactsService
    {
        private readonly IPersonContactsRepository _personContactsRepository;

        public PersonContactsService(IPersonContactsRepository personContactsRepository)
        {
            _personContactsRepository = personContactsRepository;
        }

        public async Task<IEnumerable<PersonContacts>> GetAll()
        {
            return await _personContactsRepository.GetAll();
        }

        public async Task<PersonContacts?> Get(long Id)
        {
            return await _personContactsRepository.Get(Id);
        }

        public async Task<long> Insert(PersonContacts personContacts)
        {
            return await _personContactsRepository.Insert(personContacts);
        }

        public async Task<bool> Update(PersonContacts personContacts)
        {
            return await _personContactsRepository.Update(personContacts);
        }

        public async Task<bool> Delete(long Id)
        {
            return await _personContactsRepository.Delete(Id);
        }
    }
}
