using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace cSharpBird.API;

public class UserStorageEFRepo : IUserStorageEF
{
    private readonly cSharpBirdContext _context;
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBirdWeb_DataSource.txt");
    public UserStorageEFRepo(cSharpBirdContext contextFromBuilder)
    {
        _context = contextFromBuilder;
    }
    public async Task<User?> CreateUserInDbAsync (User newUserFromService)
    {
        _context.Users.Add(newUserFromService);
        await _context.SaveChangesAsync();
        return newUserFromService;
    }
    public async Task<User?> GetUserFromDbUsername (string usernameToFind)
    {
        User? foundUser = await _context.Users.FirstOrDefaultAsync(user => user.userName == usernameToFind);
        return foundUser;
    }
    public async void WriteUpdatedUser (User updatedUser)
    {
        User? existingUser = await _context.Users.FirstOrDefaultAsync(user => user.userId == updatedUser.userId);
        if (existingUser != null)
        {
            existingUser.userName = updatedUser.userName;
            existingUser.displayName = updatedUser.displayName;
            existingUser.hashedPW = updatedUser.hashedPW;
        }
        _context.SaveChanges();
    }
    public void StoreSalt(string salt, Guid UserId)
    {
        //used to initially store salt at user creation
        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();
        
        string cmdText = "INSERT INTO salt (userId,salt) VALUES (@userId,@salt);";
        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@userId",UserId);
        cmd.Parameters.AddWithValue("@salt",salt);

        cmd.ExecuteNonQuery();
        connection.Close();
    }
    public string GetSalt(User user)
    {
        //retrieves salt to compare against sign-in
        using SqlConnection connection = new SqlConnection (_connectionstring);
        string salt = "";

        connection.Open();
        
        string cmdText = "Select salt FROM salt WHERE userId = @userId;";
        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@userId",user.userId);
        cmd.Parameters.AddWithValue("@salt",salt);

        using SqlDataReader reader = cmd.ExecuteReader();

        while(reader.Read())
        {
           salt = reader.GetString(0);
        }

        connection.Close();

        return salt;
    }
    public void UpdateSalt(string salt, Guid UserId)
    {
        //updates salt for a given user at password change
        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();

        string cmdText = "UPDATE salt SET salt = @salt WHERE userId = @userId;";
        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@userId",UserId);
        cmd.Parameters.AddWithValue("@salt",salt);

        cmd.ExecuteNonQuery();
        connection.Close();
    }
}