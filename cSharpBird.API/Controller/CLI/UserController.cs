namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

//DEPRECATED

public class UserController : ControllerBase
{
    //public static IUserStorageEF AccessUser = new UserStorageEFRepo();
    private readonly IUserStorageEF AccessUser;
    public UserController (IUserStorageEF efRepoFromBuilder)
    {
        AccessUser = efRepoFromBuilder;
    }
    public async Task<User?> CreateUserInDbAsync (User newUserFromService)
    {
        return await AccessUser.CreateUserInDbAsync(newUserFromService);
    }
    public async Task<User> GetUserFromDbUsername (string usernameToFind)
    {
        return AccessUser.GetUserFromDbUsername(usernameToFind).Result;
    }
    public async void WriteUpdatedUser(User updatedUser)
    {
        AccessUser.WriteUpdatedUser(updatedUser);
    }
    public async void StoreSalt(string salt, Guid UserId)
    {
        AccessUser.StoreSalt(salt,UserId);
    }    
    public async Task<string?> GetSalt(User user)
    {
        return AccessUser.GetSalt(user).Result;
    }
    public async void UpdateSalt(string salt, Guid UserId)
    {
        AccessUser.UpdateSalt(salt,UserId);
    }
    public async void UpdatePassword(string password1,User user)
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