namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
public class UserController
{
    public static IAccessUserFile AccessUser = new UserSQL();
    public static List<User> GetFullUserList()
    {
        List<User> userList = AccessUser.GetFullUserList();
        return userList;
    }
    public static void WriteUser(User user)
    {
        AccessUser.WriteUser(user);
    }
    public static void WriteUpdatedUser(User updatedUser)
    {
        AccessUser.WriteUpdatedUser(updatedUser);
    }
    public static void WriteCurrentUser(User user)
    {
        AccessUser.WriteCurrentUser(user);
    }
    public static User ReadCurrentUser()
    {
        //Console.WriteLine("Call to UC RCU");
        User currentSession = AccessUser.ReadCurrentUser();
        return currentSession;
    }
    public static void ClearCurrentUser()
    {
        AccessUser.ClearCurrentUser();
    }
    public static User FindUser(string user)
    {
        User foundUser = User.FindUser(user);
        return foundUser;
    }
    public static void changeEmail(User user)
    {
        User.changeEmail(user);        
    }
    public static void changeName(User user)
    {
        User.changeName(user);
    }
    public static bool ValidUserSession()
    {
        //Console.WriteLine("Call to UC VUS");
        bool valid = AccessUser.ValidUserSession();
        return valid;
    }
    public static void StoreSalt(string salt, Guid UserId)
    {
        AccessUser.StoreSalt(salt,UserId);
    }
    public static string GetSalt(User user)
    {
        string currentSession = AccessUser.GetSalt(user);
        return currentSession;
    }
    public static void UpdateSalt(string salt, Guid UserId)
    {
        AccessUser.UpdateSalt(salt,UserId);
    }
    public static void UpdatePassword(string password1,User user)
    {
        //overwrites the old password and salt with a new one as given by a verified user
        string hashedPW = "";
        hashedPW = CryptoController.HashPassword(user.userId,password1);
        user.hashedPW = hashedPW;
        WriteUpdatedUser(user);
    }
    public static bool ValidEmail(string email)
    {
        //validates email to see if it is correctly formatted and return boolean
        //credit to Josh Lee for providing the regex
        if (string.IsNullOrWhiteSpace(email))
            return false;
        if (email.EndsWith("."))
            return false;
        try
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, pattern);
        }
        catch
        {
            return false;
        }
    }
}