namespace API.DTOs;

public record LoginResponseDto(
    string UserName,
    string Token
);
