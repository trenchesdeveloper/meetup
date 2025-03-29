using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public record RegisterDto(
    [Required]
    string Name,
    [Required]
    string UserName,
    [Required]
    [StringLength(8, MinimumLength = 4)]
    string Password
);
