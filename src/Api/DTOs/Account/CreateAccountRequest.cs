using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.Account
{
    public class CreateAccountRequest
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = default!;
    }
}
