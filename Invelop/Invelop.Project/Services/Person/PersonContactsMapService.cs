using Invelop.Project.Client.Models;
using Invelop.Project.Core.Models;

namespace Invelop.Project.Client.Services.Person
{
    public class PersonContactsMapService : IPersonContactsMapService
    {
        public PersonContacts MapViewToModel(PersonContactsViewModel personContactsViewModel)
        {
            return new PersonContacts
            {
                Id = personContactsViewModel.Id,
                Address = personContactsViewModel.Address,
                DateOfBirth = personContactsViewModel.DateOfBirth?.Date,
                Firstname = personContactsViewModel.Firstname,
                IBAN = personContactsViewModel.IBAN,
                PhoneNumber = personContactsViewModel.PhoneNumber,
                Surname = personContactsViewModel.Surname
            };
        }

        public PersonContactsViewModel MapModelToView(PersonContacts personContacts)
        {
            return new PersonContactsViewModel
            {
                Id = personContacts.Id,
                Address = personContacts.Address,
                DateOfBirth = personContacts.DateOfBirth.HasValue ? new DateTimeOffset(personContacts.DateOfBirth.Value.Date, new TimeSpan()) : null,
                Firstname = personContacts.Firstname,
                IBAN = personContacts.IBAN,
                PhoneNumber = personContacts.PhoneNumber,
                Surname = personContacts.Surname
            };
        }
    }
}
