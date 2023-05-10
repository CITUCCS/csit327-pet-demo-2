using System.ComponentModel.DataAnnotations;

namespace Pets.Dto
{
    public class CreatePetDto
    {
        [Required]
        [StringLength(10)]
        public string? Name { get; set; }

        [Required]
        [Range(1,1000)]
        public int Age { get; set; }
    }
}
