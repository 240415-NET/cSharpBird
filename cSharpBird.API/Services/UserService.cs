using Microsoft.Identity.Client;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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
    public Task<User?> WriteUpdatedUser (User updatedUser)
    {
        return _userStorage.WriteUpdatedUser(updatedUser);
    }
    public Task<Guid?> StoreSalt (string salt, Guid UserId)
    {
        return _userStorage.StoreSalt(salt,UserId);
    }
    public Task<string?> GetSalt(User user)
    {
        return _userStorage.GetSalt(user);
    }
    public Task<Guid?> UpdateSalt (string salt,Guid userId)
    {
        return _userStorage.UpdateSalt(salt,userId);
    }
    public async Task<User?> UpdatePassword (string password1, User user)
    {
        user.hashedPW = HashPassword(user.userId,password1);
        await WriteUpdatedUser(user);
        return user;
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
    public async Task<User?> changeEmail(User user, string newEmail)
    {
        if (!ValidEmail(newEmail))
            throw new Exception ("Invalid email");
        else
        {
            user.userName = newEmail;
            await WriteUpdatedUser(user);
        }        
        return user;
    }
    public async Task<User?> changeName(User user, string newName)
    {
        if (String.IsNullOrEmpty(newName))
            throw new Exception ("Invalid name");
        else
        {
            await WriteUpdatedUser(user);
        }
        return user;
    }
    
    //Crypto portion follows
    const int keySize = 64;
    const int iterations = 250000;
    public string InitHashPassword(Guid UserId, string password)
    {
        //salts and hashes given password on user creation
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        StoreSalt(salt,UserId);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            keySize
        );
        return Convert.ToHexString(hash);
    }
    public async Task<Guid?> StoreSalt(byte[] salt, Guid UserId)
    {
        //stores user salt as a hex value separate from hashed passwords
        string hexSalt = Convert.ToHexString(salt);
        return await StoreSalt(hexSalt,UserId);
    }
    public string HashPassword(Guid UserId, string password)
    {
        //salts and hashes given password for an existing user
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        UpdateSalt(salt,UserId);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            iterations,
            HashAlgorithmName.SHA512,
            keySize
        );
        return Convert.ToHexString(hash);
    }
    public async Task<Guid?> UpdateSalt(byte[] salt, Guid UserId)
    {
        //stores user salt as a hex value separate from hashed passwords
        string hexSalt = Convert.ToHexString(salt);
        return await UpdateSalt(hexSalt,UserId);
    }
    public bool VerifyPassword(string password, User user)
    {
        //retrieves salt and compares hashes
        string salt = GetSalt(user).Result;

        var comparisonHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            Convert.FromHexString(salt),
            iterations,
            HashAlgorithmName.SHA512,
            keySize
        );

        return CryptographicOperations.FixedTimeEquals(comparisonHash,Convert.FromHexString(user.hashedPW));
    }
}