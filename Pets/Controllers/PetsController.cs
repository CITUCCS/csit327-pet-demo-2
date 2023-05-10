using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pets.Contexts;
using Pets.Models;
using Pets.Repositories;

namespace Pets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IRepository<Pet> _repository;

        public PetsController(IRepository<Pet> repository)
        {
            _repository = repository;
        }

        [HttpGet(Name = "GetAllPets")]
        public IActionResult GetAll()
        {
            var pets = _repository.GetAll();
            if (pets.Any())
                return Ok(pets);
            else
                return NoContent();
        }

        [HttpGet("{id}", Name = "GetPetById")]
        public IActionResult Get(int id)
        {
            var desiredPet = _repository.Get(id);

            if (desiredPet != null)
                return Ok(desiredPet);
            else
                return NotFound();
        }

        [HttpPost(Name = "CreatePet")]
        public IActionResult Create([FromBody] Pet pet)
        {
            if (pet == null)
                return BadRequest();

            if (_repository
                .GetAll()
                .Where(p => p.Name!.Equals(pet.Name))
                .FirstOrDefault() != null)
                return BadRequest($"Pet {pet.Name} already exist");

            _repository.Add(pet);

            return Ok(pet);
        }

        [HttpPut(Name = "UpdatePet")]
        public IActionResult Update([FromBody] Pet pet)
        {
            if (pet == null)
                return BadRequest();

            var desiredPet = _repository.Get(pet.Id);

            if (desiredPet == null)
            {
                _repository.Add(pet);
                return Ok(pet);
            } 
            else
            {
                _repository.Update(desiredPet, pet);
                return Ok(desiredPet);
            }
        }

        [HttpDelete("{id}", Name = "DeletePet")]
        public IActionResult Delete(int id)
        {
            var desiredPet = _repository.Get(id);

            if (desiredPet == null)
            {
                return NotFound($"No pet with id {id} exists.");
            }

            _repository.Delete(new Pet { Id = id});

            return NoContent();
        }
    }
}
