namespace cSharpBird.API;

public interface IUserService
{
    public Task<User> CreateNewUserAsync(User newUserSent);
    public Task<User> GetUserByUsernameAsync(string usernameToFind);
    public Task<bool> UserExists(string userName);
    public Task<User?> WriteUpdatedUser (User updatedUser);
    public Task<Guid?> StoreSalt (string salt, Guid UserId);
    public Task<string?> GetSalt(User user);
    public Task<Guid?> UpdateSalt (string salt,Guid userId);
    public Task<User?> UpdatePassword (string password1, User user);
    public bool ValidEmail (string email);
    public Task<User?> changeEmail(User user, string newEmail);
    public Task<User?> changeName(User user, string newName);
    
    //Crypto portion below
    const int keySize = 64;
    const int iterations = 250000;
    public Task<string?> InitHashPassword(Guid UserId, string password);
    public Task<Guid?> StoreSalt(byte[] salt, Guid UserId);
    public Task<string?> HashPassword(Guid UserId, string password);
    public Task<Guid?> UpdateSalt(byte[] salt, Guid UserId);
    public Task<bool?> VerifyPassword(string password, User user);
}