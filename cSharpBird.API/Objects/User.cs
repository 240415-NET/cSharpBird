namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public Guid userId {get; set;}
    public string userName {get; set;}
    public string displayName {get; set;}
    public string hashedPW {get; set;}
    public User() {}

    public User(string _userName,string password)
    {
        userId = Guid.NewGuid(); 
        userName = _userName;
        displayName = "";
        hashedPW = CryptoController.InitHashPassword(userId,password);
    }
    public User(Guid _userId, string _userName, string _displayName, string _hashedPW)
    {
        userId = _userId;
        userName = _userName;
        displayName = _displayName;
        hashedPW = _hashedPW;
    }

    //These will need to be relocated
        public static void changeEmail(User user)
    {
        Console.WriteLine("What would you like to change your email to?");
        string newEmail = Console.ReadLine().Trim();
        if (!UserController.ValidEmail(newEmail))
            Console.WriteLine("Email not updated");
        else
        {
            user.userName = newEmail;
            Console.WriteLine($"Email updated to {newEmail}");
            UserController.WriteUpdatedUser(user);
        }
        
    }
    public static void changeName(User user)
    {
        Console.WriteLine("What would you like to change your display name to?");
        string newName = Console.ReadLine().Trim();
        if (String.IsNullOrEmpty(newName))
            Console.WriteLine("Name not updated");
        else
        {
            user.displayName = newName;
            Console.WriteLine($"Name updated to {newName}");
            UserController.WriteUpdatedUser(user);
        }
    }
}