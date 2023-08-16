using System.ComponentModel.DataAnnotations;

namespace Entities.Models.Dtos
{
    public record UserDtoForCreation :UserDto
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Password is required")]
        public String? Password { get; init; }
    }
}