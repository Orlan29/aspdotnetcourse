using Microsoft.AspNetCore.Mvc;
using UnivAssurance.WebAPI.Logging;
using UnivAssurance.WebAPI.Models;
using UnivAssurance.DataAccess.Data;
using UnivAssurance.DataAccess.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Authorization;
using UnivAssurance.Auth.Utilities;

namespace UnivAssurance.WebAPI.Controllers
{
    /**
        Le flag ApiController ici permet de spécifier que
        le controller en question devient un controller d'API
    */

    [ApiController]
    [Route("Persons")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private const string admin = RoleUser.Admin;
        private ILog OurLogger;
        private UnivAssuranceDBContext Context;
        private PersonService personService;

        public PersonController(ILog logger, UnivAssuranceDBContext context)
        {
            OurLogger = logger;
            this.Context = context;
            this.personService = new PersonService(this.Context);
        }

        /// <summary>
        /// Find all persons
        /// </summary>
        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            this.OurLogger.Information("Tous les utilisateurs");
            
            return Ok(personService.FindAllPersons());
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("{personId}")]
        public IActionResult Get(int personId)
        {
            return Ok(personService.FindOnePersonById(personId));
        }

        [HttpPut]
        [Route("{personId}")]
        public IActionResult Update(PartialPerson person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data invalid");
            }

            Person currentPerson = new Person() {
                TypePart = person.TypePart,
                NumberTypePart = person.NumberTypePart,
                Name = person.Name,
                FirstName = person.FirstName,
                Phone = person.Phone,
                Sex = person.Sex,
                MaritalStatus = person.MaritalStatus,
                NumberChildren = person.NumberChildren,
                Employer = person.Employer
            };

            return Ok(personService.UpdateOnePersonById(currentPerson));
        }

        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RoleUser.Admin)]
        public IActionResult Create([FromBody] PartialPerson person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data invalid");
            }

            Person newPerson = new Person() {
                TypePart = person.TypePart,
                NumberTypePart = person.NumberTypePart,
                Name = person.Name,
                FirstName = person.FirstName,
                Phone = person.Phone,
                Sex = person.Sex,
                MaritalStatus = person.MaritalStatus,
                NumberChildren = person.NumberChildren,
                Employer = person.Employer
            };

            return Ok(personService.CreateOnePerson(newPerson));
        }

        [HttpDelete]
        [Route("{personId}")]
        public IActionResult Delete(int personId)
        {
            return Ok(personService.DeletOnePersonById(personId));
        }
    }
}