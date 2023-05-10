using Microsoft.EntityFrameworkCore;
using Pets.Models;

namespace Pets.Contexts
{
    public class PetContext : DbContext
    {
        public PetContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
    }
}
