using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace cSharpBird.API;

public class UserStorageEFRepo : IUserStorageEF
{
    private readonly cSharpBirdContext _context;
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBirdWeb_DataSource.txt");
    public UserStorageEFRepo(cSharpBirdContext contextFromBuilder)
    {
        _context = contextFromBuilder;
    }
    public async Task<User?> CreateUserInDbAsync (User newUserFromService)
    {
        _context.Users.Add(newUserFromService);
        await _context.SaveChangesAsync();
        return newUserFromService;
    }
    public async Task<User?> GetUserFromDbUsername (string usernameToFind)
    {
        User? foundUser = await _context.Users.FirstOrDefaultAsync(user => user.userName == usernameToFind);
        return foundUser;
    }
    public async void WriteUpdatedUser (User updatedUser)
    {
        User? existingUser = await _context.Users.FirstOrDefaultAsync(user => user.userId == updatedUser.userId);
        if (existingUser != null)
        {
            existingUser.userName = updatedUser.userName;
            existingUser.displayName = updatedUser.displayName;
            existingUser.hashedPW = updatedUser.hashedPW;
        }
        _context.SaveChanges();
    }
    public async void StoreSalt(string salt, Guid UserId)
    {
        Salt _salt = new Salt (UserId,salt);
        _context.Salts.Add(_salt);
        await _context.SaveChangesAsync();
    }
    public async Task<string?> GetSalt(User user)
    {
        Salt? salt = await _context.Salts.FirstOrDefaultAsync(salt => salt.userId == user.userId);
        return salt.salt;
    }
    public async void UpdateSalt(string salt, Guid UserId)
    {
        Salt _salt = new Salt (UserId,salt);
        Salt? currentSalt = await _context.Salts.FirstOrDefaultAsync(salt => salt.userId == _salt.userId);
        if (currentSalt != null)
        {
            currentSalt.salt = _salt.salt;
        }
        _context.SaveChanges();
    }
}