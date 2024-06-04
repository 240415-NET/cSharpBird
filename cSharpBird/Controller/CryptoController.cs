namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
public class CryptoController
{
    const int keySize = 64;
    const int iterations = 250000;
    public static string InitHashPassword(Guid UserId, string password)
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
    public static void StoreSalt(byte[] salt, Guid UserId)
    {
        //stores user salt as a hex value separate from hashed passwords
        string hexSalt = Convert.ToHexString(salt);
        UserController.StoreSalt(hexSalt,UserId);
    }
    public static string HashPassword(Guid UserId, string password)
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
    public static void UpdateSalt(byte[] salt, Guid UserId)
    {
        //stores user salt as a hex value separate from hashed passwords
        string hexSalt = Convert.ToHexString(salt);
        UserController.UpdateSalt(hexSalt,UserId);
    }
    public static bool VerifyPassword(string password, User user)
    {
        //retrieves salt and compares hashes
        string salt = UserController.GetSalt(user);

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