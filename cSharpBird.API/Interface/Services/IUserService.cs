namespace cSharpBird.API;

public interface IUserService
{
    public Task<User> CreateNewUserAsync(User newUserSent);
    public Task<User> GetUserByUsernameAsync(string usernameToFind);
}