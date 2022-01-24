﻿using Invelop.Project.Client.Models;
using Microsoft.AspNetCore.Mvc;

namespace Invelop.Project.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonContactsController : ControllerBase
    {
        private readonly ILogger<PersonContactsController> _logger;

        public PersonContactsController(ILogger<PersonContactsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(new List<PersonContacts>());
        }

        [HttpGet]
        [Route("/{personId}")]
        public async Task<IActionResult> Get([FromQuery] int personId)
        {
            return Ok(new PersonContacts());
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] PersonContacts personContacts)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("/{personId}")]
        public async Task<IActionResult> Update([FromQuery] int personId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("/{personId}")]
        public async Task<IActionResult> Delete([FromQuery] int personId)
        {
            throw new NotImplementedException();
        }
    }
}
