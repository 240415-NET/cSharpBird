namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
public interface ICryptoController
{
    const int keySize = 64;
    const int iterations = 250000;
    public string InitHashPassword(Guid UserId, string password);

    public void StoreSalt(byte[] salt, Guid UserId);

    public string HashPassword(Guid UserId, string password);

    public void UpdateSalt(byte[] salt, Guid UserId);

    public bool VerifyPassword(string password, User user);

}