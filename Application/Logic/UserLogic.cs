using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;
// oh no
public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }
    
    public async Task<User> CreateAsync(UserCreationDTO dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.Username);
        if (existing != null)
            throw new Exception("Username already taken!");

        ValidateData(dto);
        User toCreate = new User
        {
            Username = dto.Username
        };

        User created = await userDao.CreateAsync(toCreate);

        return created;
    }

    private static void ValidateData(UserCreationDTO userToCreate)
    {
        string username = userToCreate.Username;

        if (username.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (username.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
    }

}
