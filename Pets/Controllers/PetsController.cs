using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pets.Contexts;
using Pets.Dto;
using Pets.Models;
using Pets.Repositories;
using Pets.Services;

namespace Pets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet(Name = "GetAllPets")]
        public IActionResult GetAll()
        {
            var pets = _petService.GetAllPets();
            if (pets.Any())
                return Ok(pets);
            else
                return NoContent();
        }

        [HttpGet("{id}", Name = "GetPetById")]
        public IActionResult Get(int id)
        {
            var desiredPet = _petService.GetPetById(id);

            if (desiredPet != null)
                return Ok(desiredPet);
            else
                return NotFound();
        }

        [HttpPost(Name = "CreatePet")]
        public IActionResult Create([FromBody] CreatePetDto pet)
        {
            if (pet == null)
                return BadRequest();

            try
            {
                _petService.AddPet(pet);
            }
            catch (Exception)
            {
                return BadRequest($"Pet {pet.Name} already exist");
            }

            return Ok(pet);
        }

        [HttpPut(Name = "UpdatePet")]
        public IActionResult Update([FromBody] PetDto pet)
        {
            if (pet == null)
                return BadRequest();

            var updatedOrCreatedPet = _petService.Update(pet);

            return Ok(updatedOrCreatedPet);
        }

        [HttpDelete("{id}", Name = "DeletePet")]
        public IActionResult Delete(int id)
        {
            try
            {
                _petService.Delete(id);
            }
            catch (Exception e)
            {

                return NotFound(e.Message);
            }

            return NoContent();
        }
    }
}
