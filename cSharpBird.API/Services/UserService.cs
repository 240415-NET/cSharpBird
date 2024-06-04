using Microsoft.Identity.Client;

namespace cSharpBird.API;

public class UserService : IUserService
{
    private readonly IUserStorageEF _userStorage;
    public UserService (IUserStorageEF efRepoFromBuilder)
    {
        _userStorage = efRepoFromBuilder;
    }
    public async Task<User> CreateNewUserAsync(User newUserSent)
    {
        if (UserExists(newUserSent.userName) == true)
        {
            throw new Exception("Email already in use");
        }
        if (String.IsNullOrEmpty(newUserSent.userName) == true)
        {
            throw new Exception("Email cannot be blank");
        }
        if (UserController.ValidEmail(newUserSent.userName) == false)
        {
            throw new Exception("Invalid email");
        }
        await _userStorage.CreateUserInDbAsync(newUserSent);
        return newUserSent;
    }
    public async Task<User> GetUserByUsernameAsync(string usernameToFind)
    {
        if(String.IsNullOrEmpty(usernameToFind))
        {
            throw new Exception("Email cannot be blank");
        }
        try
        {
            User? foundUser = await _userStorage.GetUserFromDbUsername(usernameToFind);
            if (foundUser == null)
            {
                throw new Exception("User not found");
            }
            return foundUser;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public bool UserExists(string userName)
    {
        User searchedUser = UserController.GetUserFromDbUsername(userName).Result;
        if (userName == searchedUser.userName)
            return true;
        else
            return false;
    }
}