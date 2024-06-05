namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
public interface IUserStorageEF
{
    public Task<User?> CreateUserInDbAsync (User newUserFromService);
    public Task<User?> GetUserFromDbUsername (string usernameToFind);
    public void WriteUpdatedUser(User updatedUser);
    public void StoreSalt(string salt, Guid UserId);    
    public Task<string?> GetSalt(User user);
    public void UpdateSalt(string salt, Guid UserId);
}