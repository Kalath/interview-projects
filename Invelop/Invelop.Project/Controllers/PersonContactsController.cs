using Invelop.Project.Client.Models;
using Invelop.Project.Core.Models;
using Invelop.Project.Services.Person;
using Microsoft.AspNetCore.Mvc;

namespace Invelop.Project.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonContactsController : ControllerBase
    {
        private readonly ILogger<PersonContactsController> _logger;
        private readonly IPersonContactsService _personContactsService;

        public PersonContactsController(ILogger<PersonContactsController> logger, IPersonContactsService personContactsService)
        {
            _logger = logger;
            _personContactsService = personContactsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personsContacts = await _personContactsService.GetAll();

            if (personsContacts != default)
            {
                var responseModel = personsContacts.Select(p => MapModelToView(p));
                return Ok(responseModel);
            }

            return Ok(default);
        }

        [HttpGet("{Id}", Name = "GetPersonContacts")]
        public async Task<IActionResult> Get(long Id)
        {
            var personContacts = await _personContactsService.Get(Id);

            return personContacts != default ? Ok(MapModelToView(personContacts)) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PersonContactsViewModel personContactsViewModel)
        {
            var personContacts = MapViewToModel(personContactsViewModel);

            var newId = await _personContactsService.Insert(personContacts);

            if (newId != default)
            {
                return CreatedAtRoute("GetPersonContacts", new { Id = newId }, newId);
            }

            return StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PersonContactsViewModel personContactsViewModel)
        {
            var personContacts = MapViewToModel(personContactsViewModel);
            var updated = await _personContactsService.Update(personContacts);

            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _personContactsService.Delete(id);

            return deleted ? NoContent() : BadRequest();
        }

        private static PersonContacts MapViewToModel(PersonContactsViewModel personContactsViewModel)
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

        private static PersonContactsViewModel MapModelToView(PersonContacts personContacts)
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
