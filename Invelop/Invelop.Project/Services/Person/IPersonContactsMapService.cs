using Invelop.Project.Client.Models;
using Invelop.Project.Core.Models;

namespace Invelop.Project.Client.Services.Person
{
    public interface IPersonContactsMapService
    {
        PersonContactsViewModel MapModelToView(PersonContacts personContacts);
        PersonContacts MapViewToModel(PersonContactsViewModel personContactsViewModel);
    }
}