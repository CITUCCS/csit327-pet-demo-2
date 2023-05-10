using Pets.Dto;
using Pets.Models;

namespace Pets.Services
{
    public interface IPetService
    {
        IEnumerable<PetDto> GetAllPets();
        PetDto GetPetById(int id);
        void AddPet(CreatePetDto pet);
        PetDto Update(PetDto pet);
        void Delete(int id);
    }
}
