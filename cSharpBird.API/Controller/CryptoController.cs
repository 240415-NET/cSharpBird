namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
public class CryptoController : ICryptoController
{
    const int keySize = 64;
    const int iterations = 250000;
    private readonly IUserService _userService;
    public CryptoController (IUserService userServiceFromBuilder)
    {
        _userService = userServiceFromBuilder;
    }
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
    public void StoreSalt(byte[] salt, Guid UserId)
    {
        //stores user salt as a hex value separate from hashed passwords
        string hexSalt = Convert.ToHexString(salt);
        _userService.StoreSalt(hexSalt,UserId);
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
    public void UpdateSalt(byte[] salt, Guid UserId)
    {
        //stores user salt as a hex value separate from hashed passwords
        string hexSalt = Convert.ToHexString(salt);
        _userService.UpdateSalt(hexSalt,UserId);
    }
    public bool VerifyPassword(string password, User user)
    {
        //retrieves salt and compares hashes
        string salt = _userService.GetSalt(user).Result;

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