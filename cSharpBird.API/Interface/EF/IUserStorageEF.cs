namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
public interface IUserStorageEF
{
    public Task<User?> CreateUserInDbAsync (User newUserFromService);
    public Task<User?> GetUserFromDbUsername (string usernameToFind);
    public Task<User?> WriteUpdatedUser(User updatedUser);
    public Task<Guid?> StoreSalt(string salt, Guid UserId);    
    public Task<string?> GetSalt(User user);
    public Task<Guid?> UpdateSalt(string salt, Guid UserId);
}