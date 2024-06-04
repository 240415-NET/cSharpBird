namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
public interface IUserStorageEF
{
    public Task<User?> GetUserFromDbUsername (string usernameToFind);

    public Task<User?> CreateUserinDbAsync (User newUserFromService);

    public void WriteUpdatedUser(User updatedUser);

    public void WriteCurrentUser(User user);

    public User ReadCurrentUser();

    public void ClearCurrentUser();
    
    public bool ValidUserSession();

    public void StoreSalt(string salt, Guid UserId);
    
    public string GetSalt(User user);
    public void UpdateSalt(string salt, Guid UserId);
}