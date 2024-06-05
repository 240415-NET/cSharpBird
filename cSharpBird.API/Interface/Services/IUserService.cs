namespace cSharpBird.API;

public interface IUserService
{
    public Task<User> CreateNewUserAsync(User newUserSent);
    public Task<User> GetUserByUsernameAsync(string usernameToFind);
    public Task<bool> UserExists(string userName);
    public void WriteUpdatedUser (User updatedUser);
    public void StoreSalt (string salt, Guid UserId);
    public Task<string?> GetSalt(User user);
    public void UpdateSalt (string salt,Guid userId);
    public void UpdatePassword (string password1, User user);
    public bool ValidEmail (string email);
    public void changeEmail(User user);
    public void changeName(User user);
    
    //Crypto portion below
    const int keySize = 64;
    const int iterations = 250000;
    public string InitHashPassword(Guid UserId, string password);
    public void StoreSalt(byte[] salt, Guid UserId);
    public string HashPassword(Guid UserId, string password);
    public void UpdateSalt(byte[] salt, Guid UserId);
    public bool VerifyPassword(string password, User user);
}