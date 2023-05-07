using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pets.Contexts;
using Pets.Models;

namespace Pets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PetsController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet(Name = "GetAllPets")]
        public IActionResult GetAll()
        {
            var pets = _dataContext.Pets.ToList();
            if (pets.Any())
                return Ok(pets);
            else
                return NoContent();
        }

        [HttpGet("{id}", Name = "GetPetById")]
        public IActionResult Get(int id)
        {
            var desiredPet = _dataContext.Pets
                .Where(p => p.Id == id)
                .FirstOrDefault();

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

            if (_dataContext.Pets.Where(p => p.Name!.Equals(pet.Name)).FirstOrDefault() != null)
                return BadRequest($"Pet {pet.Name} already exist");

            _dataContext.Pets.Add(pet);
            _dataContext.SaveChanges();

            return Ok(pet);
        }

        [HttpPut(Name = "UpdatePet")]
        public IActionResult Update([FromBody] Pet pet)
        {
            if (pet == null)
                return BadRequest();

            var desiredPet = _dataContext.Pets.Where(p => p.Id == pet.Id).FirstOrDefault();

            if (desiredPet == null)
            {
                _dataContext.Pets.Add(pet);
                _dataContext.SaveChanges();
                return Ok(pet);
            } 
            else
            {
                desiredPet.Name = pet.Name;
                desiredPet.Age = pet.Age;
                _dataContext.SaveChanges();
                return Ok(desiredPet);
            }
        }

        [HttpDelete("{id}", Name = "DeletePet")]
        public IActionResult Delete(int id)
        {
            var desiredPet = _dataContext.Pets.Where(p => p.Id == id).FirstOrDefault();

            if (desiredPet == null)
            {
                return NotFound($"No pet with id {id} exists.");
            }

            _dataContext.Pets.Remove(desiredPet);
            _dataContext.SaveChanges();

            return NoContent();
        }
    }
}
