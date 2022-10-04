namespace Shared.DTOs;

public class UserCreationDTO
{
    public string Username { get; }

    public UserCreationDTO(string username)
    {
        Username = username;
    }
    
}