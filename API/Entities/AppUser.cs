namespace API.Entities;

public class AppUser
{
    public int Id { get; init; }
    
    public required string Username { get; set; }

    public required string Name { get; set; }
}