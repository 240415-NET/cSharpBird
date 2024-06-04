namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class UserCreation
{
    public static void CreateUser()
    {
        //collects email for user creation, validates it is not a duplicate, and confirms user creation
        string email = "";
        string password = "";
        bool exitLoop = false;

        Console.Clear();
        do
        {
            UserInterface.WriteColors("Please enter your {=Green}email{/} to create a new account\n");
            email = Console.ReadLine().Trim();
            if (String.IsNullOrEmpty(email))
            {
                Console.Clear();
                UserInterface.WriteColors("{=Green}Email{/} cannot be blank. Please try again\n");
            }
            else if(UserCreation.UserDupe(email)) //prevents duplicate sign-ons
            {
                exitLoop = true;
                UserInterface.WriteColors("{=Green}Email{/} already in use. Please sign in\n");
                AcctAccess.LogIn();
            }
            else if (!UserController.ValidEmail(email)) //since this returns true for a valid email, needs to be inverted
            {
                Console.Clear();
                UserInterface.WriteColorsLine("{=Red}"+email+"{/} is not a valid email. Please try again");
            }
            else
            {
                exitLoop = true;
                UserInterface.WriteColorsLine("Please enter your desired {=Green}password{/}");
                password = Console.ReadLine().Trim();
                User currentSession = NewUser(email,password);
                UserInterface.WriteColors("{=Blue}New user{/} created for " + email +"\n");
                UserMaintenance.UserMenu(currentSession);
            }
        }
        while (exitLoop == false);
    }
    public static bool UserDupe(string userRequested)
    {
        string email = userRequested;
        //This method is used to find the user attempting to sign in and return the proper user object
        //Will return full user list
        List<User> userList = UserController.GetFullUserList();
        User foundUser = userList.FirstOrDefault(u => u.userName.ToLower() == email.ToLower());
        if (foundUser == null)
            return false;
        else
            return true;
    }
    public static User NewUser(string email, string password)
    {
        //creates user object and calls to write to file
        User newUser = new User(email, password);
        UserController.WriteUser(newUser);
        return newUser;
    }
}