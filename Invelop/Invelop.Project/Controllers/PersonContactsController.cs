using Invelop.Project.Client.Models;
using Invelop.Project.Client.Services.Person;
using Invelop.Project.Services.Person;
using Microsoft.AspNetCore.Mvc;

namespace Invelop.Project.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonContactsController : ControllerBase
    {
        private readonly IPersonContactsService _personContactsService;
        private readonly IPersonContactsMapService _personContactsMapService;

        public PersonContactsController(IPersonContactsService personContactsService, IPersonContactsMapService personContactsMapService)
        {
            _personContactsService = personContactsService;
            _personContactsMapService = personContactsMapService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var personsContacts = await _personContactsService.GetAll();

            if (personsContacts != default && personsContacts.Any())
            {
                var responseModel = personsContacts.Select(p => _personContactsMapService.MapModelToView(p));
                return Ok(responseModel);
            }

            return Ok(default);
        }

        [HttpGet("{Id}", Name = "GetPersonContacts")]
        public async Task<IActionResult> Get(long Id)
        {
            var personContacts = await _personContactsService.Get(Id);

            return personContacts != default ? Ok(_personContactsMapService.MapModelToView(personContacts)) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PersonContactsViewModel personContactsViewModel)
        {
            var personContacts = _personContactsMapService.MapViewToModel(personContactsViewModel);

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
            var personContacts = _personContactsMapService.MapViewToModel(personContactsViewModel);
            var updated = await _personContactsService.Update(personContacts);

            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _personContactsService.Delete(id);

            return deleted ? NoContent() : BadRequest();
        }
    }
}
