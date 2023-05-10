using Pets.Contexts;
using Pets.Models;

namespace Pets.Repositories
{
    public class PetRepository : IRepository<Pet>
    {
        private readonly PetContext _context;

        public PetRepository(PetContext context)
        {
            _context = context;
        }

        public Pet Get(int id)
        {
            return _context.Pets.Where(p => p.Id == id).FirstOrDefault();
        }

        public IEnumerable<Pet> GetAll()
        {
            return _context.Pets.ToList();
        }

        public void Add(Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
        }

        public void Delete(Pet pet)
        {
            _context.Pets.Remove(pet);
        }

        public void Update(Pet pet, Pet updatePet)
        {
            pet.Name = updatePet.Name;
            pet.Age = updatePet.Age;
            _context.SaveChanges();
        }
    }
}
