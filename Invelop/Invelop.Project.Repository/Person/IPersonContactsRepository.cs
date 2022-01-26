using Invelop.Project.Core.Models;

namespace Invelop.Project.Repository.Person
{
    public interface IPersonContactsRepository
    {
        Task<bool> Delete(long Id);
        Task<PersonContacts?> Get(long Id);
        Task<IEnumerable<PersonContacts>> GetAll();
        Task<long> Insert(PersonContacts personContacts);
        Task<bool> Update(PersonContacts personContacts);
    }
}