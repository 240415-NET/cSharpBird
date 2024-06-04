namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;

public class UserSQL : IAccessUserFile
{
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");
    public List<User> GetFullUserList()
    {
        //retrieves full list of users to check valid userids for sign-in
        string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");

        //Credit to the Team2 for troubleshooting the SQL Data connection

        List<User> userArchive = new List<User>();

        using SqlConnection connection = new SqlConnection(_connectionstring);
        connection.Open();

        string cmdText = "SELECT userId, userName, displayName, hashedPW FROM users;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);
        using SqlDataReader reader = cmd.ExecuteReader();
     
        while(reader.Read())
        {
            if (reader.GetString(2) != "NULL")
                userArchive.Add(new User(reader.GetGuid(0),reader.GetString(1),reader.GetString(2),reader.GetString(3)));
            else
                userArchive.Add(new User(reader.GetGuid(0),reader.GetString(1),null,reader.GetString(3)));
        }
        connection.Close();
        return userArchive;
    }

    public void WriteUser(User user)
    {
        //used in user creation to create new record
        string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");

        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();

        string cmdText = "INSERT INTO users (userId,userName,displayName,hashedPW) VALUES (@userId,@userName,@displayName,@hashedPW);";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@userId",user.userId);
        cmd.Parameters.AddWithValue("@userName",user.userName);
        if (user.displayName != null)
            cmd.Parameters.AddWithValue("@displayName",user.displayName);
        else
            cmd.Parameters.AddWithValue("@displayName","NULL");
        cmd.Parameters.AddWithValue("@hashedPW",user.hashedPW);

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public void WriteUpdatedUser(User user)
    {
        //updates user record when changed from the user update menu
        string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");

        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();

        string cmdText = "UPDATE users SET userName = @userName, displayName = @displayName, hashedPW = @hashedPW WHERE userId = @userId;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@userId",user.userId);
        cmd.Parameters.AddWithValue("@userName",user.userName);
        if (user.displayName != null)
            cmd.Parameters.AddWithValue("@displayName",user.displayName);
        else
            cmd.Parameters.AddWithValue("@displayName","NULL");
        cmd.Parameters.AddWithValue("@hashedPW",user.hashedPW);

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public void WriteCurrentUser(User user)
    {
        //writes the current user in the proper table for retrieval at multiple steps or to retain for future sign-ins
        string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");

        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();

        ClearCurrentUser();

        string cmdText = "INSERT INTO currentUser (userId,userName,displayName,hashedPW) VALUES (@userId,@userName,@displayName,@hashedPW);";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.Parameters.AddWithValue("@userId",user.userId);
        cmd.Parameters.AddWithValue("@userName",user.userName);
        if (user.displayName != null)
            cmd.Parameters.AddWithValue("@displayName",user.displayName);
        else
            cmd.Parameters.AddWithValue("@displayName","NULL");
        cmd.Parameters.AddWithValue("@hashedPW",user.hashedPW);

        cmd.ExecuteNonQuery();
        connection.Close();
    }

    public User ReadCurrentUser()
    {
        //reads current user either to continue session or at various stages to ensure items are passed around correctly.
        string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");
        User currentUser = new User();
        using SqlConnection connection = new SqlConnection(_connectionstring);
        connection.Open();

        string cmdText = "SELECT userId, userName, displayName, hashedPW FROM currentUser;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);
        using SqlDataReader reader = cmd.ExecuteReader();
        while(reader.Read())
        {
            if (reader.GetString(2) != null)
                currentUser = new User(reader.GetGuid(0),reader.GetString(1),reader.GetString(2),reader.GetString(3));
            else
                currentUser = new User(reader.GetGuid(0),reader.GetString(1),null,reader.GetString(3));
        }

        connection.Close();

        return currentUser;
    }

    public void ClearCurrentUser()
    {
        //clears all records from current user table
        string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBird_DataSource.txt");

        using SqlConnection connection = new SqlConnection (_connectionstring);

        connection.Open();

        string cmdText = "DELETE FROM currentUser;";

        using SqlCommand cmd = new SqlCommand(cmdText,connection);

        cmd.ExecuteNonQuery();
        connection.Close();
    }
    
    public bool ValidUserSession()
    {
        //checks to see if valid session exists
        User user = new User();
        try
        {
            user = UserController.ReadCurrentUser();

            Guid badUID = new Guid("00000000-0000-0000-0000-000000000000");
            if (user.userId == badUID){
                return false;
            }
            else{
                return true;
            }
                
        }
        catch (Exception e){
            Console.WriteLine("Exception!!!");
            Console.WriteLine(e.StackTrace);
            return false;
        }
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