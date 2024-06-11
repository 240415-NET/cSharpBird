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
        Console.WriteLine("Create User method called");
        Console.WriteLine("Calling User Exists");
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
        public async Task<User> GetUserByGuidAsync(Guid userId)
    {
        try
        {
            User? foundUser = await _userStorage.GetUserFromDbGuid(userId);
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
        Console.WriteLine("Successful call to UserExists");
        User? searchedUser = _userStorage.GetUserFromDbUsername(userName).Result;
        Console.WriteLine("searched for user");
        if (searchedUser == null)
            return false;
        else if (userName == searchedUser.userName)
            return true;
        else
            return false;
    }
    public async Task<User?> WriteUpdatedUser (User updatedUser)
    {
        return await _userStorage.WriteUpdatedUser(updatedUser);
    }
    public async Task<Guid?> StoreSalt (string salt, Guid UserId)
    {
        return await _userStorage.StoreSalt(salt,UserId);
    }
    public async Task<string?> GetSalt(User user)
    {
        return await _userStorage.GetSalt(user);
    }
    public async Task<Guid?> UpdateSalt (string salt,Guid userId)
    {
        return await _userStorage.UpdateSalt(salt,userId);
    }
    public async Task<User?> UpdatePassword (string password1, User user)
    {
        user.hashedPW = HashPassword(user.userId,password1).Result;
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
    public async Task<string?> InitHashPassword(Guid UserId, string password)
    {
        //salts and hashes given password on user creation
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        await StoreSalt(salt,UserId);

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
    public async Task<string?> HashPassword(Guid UserId, string password)
    {
        //salts and hashes given password for an existing user
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        await UpdateSalt(salt,UserId);

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
    public async Task<bool?> VerifyPassword(string password, User user)
    {
        //retrieves salt and compares hashes
        string salt = "";
        salt = GetSalt(user).Result;

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