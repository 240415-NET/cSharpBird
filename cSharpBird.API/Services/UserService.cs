using Microsoft.Identity.Client;
using System.Text.RegularExpressions;


namespace cSharpBird.API;

public class UserService : IUserService
{
    private readonly IUserStorageEF _userStorage;
    public UserService (IUserStorageEF efRepoFromBuilder)
    {
        _userStorage = efRepoFromBuilder;
    }
    private readonly ICryptoController CryptoController = new CryptoController();
    public async Task<User> CreateNewUserAsync(User newUserSent)
    {
        if (UserExists(newUserSent.userName).Result == true)
        {
            throw new Exception("Email already in use");
        }
        if (String.IsNullOrEmpty(newUserSent.userName) == true)
        {
            throw new Exception("Email cannot be blank");
        }
        if (ValidEmail(newUserSent.userName) == false)
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
    public async Task<bool> UserExists(string userName)
    {
        User searchedUser = _userStorage.GetUserFromDbUsername(userName).Result;
        if (userName == searchedUser.userName)
            return true;
        else
            return false;
    }
    public void WriteUpdatedUser (User updatedUser)
    {
        _userStorage.WriteUpdatedUser(updatedUser);
    }
    public void StoreSalt (string salt, Guid UserId)
    {
        _userStorage.StoreSalt(salt,UserId);
    }
    public Task<string?> GetSalt(User user)
    {
        return _userStorage.GetSalt(user);
    }
    public void UpdateSalt (string salt,Guid userId)
    {
        _userStorage.UpdateSalt(salt,userId);
    }
    public void UpdatePassword (string password1, User user)
    {
        user.hashedPW = CryptoController.HashPassword(user.userId,password1);
        WriteUpdatedUser(user);
    }
    public bool ValidEmail (string email)
    {
        //validates email to see if it is correctly formatted and return boolean
        //credit to Josh Lee for providing the regex
        if (string.IsNullOrWhiteSpace(email))
            return false;
        if (email.EndsWith("."))
            return false;
        try
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        catch
        {
            return false;
        }
    }
    public void changeEmail(User user)
    {
        Console.WriteLine("What would you like to change your email to?");
        string newEmail = Console.ReadLine().Trim();
        if (!ValidEmail(newEmail))
            Console.WriteLine("Email not updated");
        else
        {
            user.userName = newEmail;
            Console.WriteLine($"Email updated to {newEmail}");
            WriteUpdatedUser(user);
        }        
    }
    public void changeName(User user)
    {
        Console.WriteLine("What would you like to change your display name to?");
        string newName = Console.ReadLine().Trim();
        if (String.IsNullOrEmpty(newName))
            Console.WriteLine("Name not updated");
        else
        {
            user.displayName = newName;
            Console.WriteLine($"Name updated to {newName}");
            WriteUpdatedUser(user);
        }
    }
}