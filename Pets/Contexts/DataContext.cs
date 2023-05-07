using Microsoft.EntityFrameworkCore;
using Pets.Models;

namespace Pets.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
    }
}
