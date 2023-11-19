using EKO.ToDoApp.AppLogic.Services.Contracts;
using EKO.ToDoApp.Infrastructure.Storage;
using EKO.ToDoApp.Shared.Entities;
using EKO.ToDoApp.Shared.Exceptions;
using EKO.ToDoApp.Shared.Requests.Users;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace EKO.ToDoApp.AppLogic.Services;

public sealed class UserService : IUserService
{
    private readonly IConfiguration _configuration;

    public UserService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<UserEntity> CreateNewUser(RegisterUserRequest newUser)
    {
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var password = HashPassword(newUser.Password, salt);

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            FirstName = newUser.FirstName,
            LastName = newUser.LastName,
            Email = newUser.Email,
            Password = Convert.ToBase64String(password),
            Salt = Convert.ToBase64String(salt)
        };

        var defaultList = new TaskList
        {
            Id = Guid.NewGuid(),
            Title = "Inbox",
            UserId = user.Id
        };

        using ToDoDbContext ctx = new(_configuration);

        if (await ctx.Users.AnyAsync(x => x.Email == user.Email))
        {
            throw new UserAlreadyExistsException($"User with email {user.Email} already exists!");
        }

        _ = await ctx.Users.AddAsync(user);

        _ = await ctx.ToDoLists.AddAsync(defaultList);

        await ctx.SaveChangesAsync();

        return user;
    }

    public async Task DeleteUser(Guid id)
    {
        using ToDoDbContext ctx = new(_configuration);

        var user = await ctx.Users.SingleOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            throw new UserNotFoundException($"Given user with ID {id} was not found!");
        }

        _ = ctx.Users.Remove(user);

        await ctx.SaveChangesAsync();
    }

    public async Task<UserEntity> GetUserByEmailAndPassword(string email, string password)
    {
        using ToDoDbContext ctx = new(_configuration);

        var user = await ctx.Users.SingleOrDefaultAsync(x => x.Email == email);

        if (user is null)
        {
            throw new UserNotFoundException($"Given user with email '{email}' was not found!");
        }

        var salt = Convert.FromBase64String(user.Salt);

        var hashedPassword = Convert.ToBase64String(HashPassword(password, salt));

        if (user.Password == hashedPassword)
        {
            return user;
        }
        else
        {
            throw new InvalidPasswordException($"Given user with email '{email}' was not found!");
        }

    }

    public async Task<IEnumerable<UserEntity>> GetAllUsers()
    {
        using ToDoDbContext ctx = new(_configuration);

        return await ctx.Users.ToListAsync();
    }

    public async Task<UserEntity> UpdateUser(UserEntity updatedUser)
    {
        using ToDoDbContext ctx = new(_configuration);

        var user = await ctx.Users.SingleOrDefaultAsync(x => x.Id == updatedUser.Id);

        if (user is null)
        {
            throw new UserNotFoundException($"Given user with ID {updatedUser.Id} was not found!");
        }

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;

        await ctx.SaveChangesAsync();

        return user;
    }

    /// <summary>
    /// Helps to hash the password.
    /// </summary>
    /// <param name="password">Password to hash</param>
    /// <param name="salt">Salt that will be added</param>
    /// <returns>Hashed password as <see cref="byte[]"/></returns>
    private byte[] HashPassword(string password, byte[] salt)
    {
        return KeyDerivation.Pbkdf2(password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256, iterationCount: 1_000, numBytesRequested: 256 / 8);
    }
}
