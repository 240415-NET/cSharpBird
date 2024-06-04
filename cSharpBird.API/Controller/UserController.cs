namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
public class UserController
{
    //public static IUserStorageEF AccessUser = new UserStorageEFRepo();
    private readonly IUserStorageEF _userStorage;
    public UserController (IUserStorageEF efRepoFromBuilder)
    {
        _userStorage = efRepoFromBuilder;
    }
    public static Task<User?> CreateUserInDbAsync (User newUserFromService)
    {
        return AccessUser.CreateUserInDbAsync(newUserFromService);
    }
    public static Task<User?> GetUserFromDbUsername (string usernameToFind)
    {
        return AccessUser.GetUserFromDbUsername(usernameToFind);
    }
    public static void WriteUpdatedUser(User updatedUser)
    {
        AccessUser.WriteUpdatedUser(updatedUser);
    }
    public static void StoreSalt(string salt, Guid UserId)
    {
        AccessUser.StoreSalt(salt,UserId);
    }    
    public static Task<string?> GetSalt(User user)
    {
        return AccessUser.GetSalt(user);
    }
    public static void UpdateSalt(string salt, Guid UserId)
    {
        AccessUser.UpdateSalt(salt,UserId);
    }
    public static void UpdatePassword(string password1,User user)
    {
        //overwrites the old password and salt with a new one as given by a verified user
        string hashedPW = "";
        hashedPW = CryptoController.HashPassword(user.userId,password1);
        user.hashedPW = hashedPW;
        WriteUpdatedUser(user);
    }
    public static bool ValidEmail(string email)
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
}