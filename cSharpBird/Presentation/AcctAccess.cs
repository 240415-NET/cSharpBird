namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class AcctAccess
{
    public static void initMenu()
    {
        //This method prompts for sign-in or account creation and routes requests appropriately.

        string[] initialPrompt = {"In this program, actionable objects are highlighted in colors","Key the highlighted text to make a selection","{=Green}1. Existing{/} User","{=Yellow}2. New{/} User","{=Red}3. Exit{/}"};
        string userSelect = "";
        bool valid = false;
        string email;

        Console.Clear();
        UserInterface.menuPrintBase(initialPrompt);
        
        do 
        {
            try
            {
                userSelect = Console.ReadLine().Trim().ToLower();
                switch (userSelect)
                {
                    case "1":
                    case "1.":
                    case "1. existing":
                    case "1. exist":
                    case "existing":
                    case "exist":
                    case "log in":
                    case "login":
                    case "sign in":
                    case "signin":
                    valid = true;
                    ContinueSession(); //verifies if user is still signed in and gives option to continue session; otherwise routes to LogIn()
                    break;
                    case "2":
                    case "2.":
                    case "2. new":
                    case "new":
                    case "new user":
                    valid = true;
                    UserCreation.CreateUser();
                    break;
                    case "3":
                    case "3.":
                    case "3. exit":
                    case "3. quit":
                    case "exit":
                    case "quit":
                    UserInterface.exit();
                    Console.WriteLine("Please enter a selection to continue");
                    break;
                    default:
                    Console.WriteLine("Please enter valid selection");
                    break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}. Please key in valid selection");
            }
        }
        while (valid == false);
    }
    public static void ContinueSession()
    {
        //this method determines if an existing user session is still present (think of a browser cookie) and continues if available
        //if it's not, it will route the user to log in to their account
        if (UserController.ValidUserSession())
        {
            User currentSession = UserController.ReadCurrentUser();
            bool validInput = false;
            string tempName = null;
            if (currentSession.displayName == null)
                tempName = currentSession.userName;
            else
                tempName = currentSession.displayName;
            do
            {
                UserInterface.WriteColorsLine("Continue with prior session as {=Green}"+tempName+"?{/} Please key yes or no");
                string userInput = Console.ReadLine().Trim().ToLower();
                if (userInput == "yes" || userInput == "y" || userInput == "continue" || userInput == "c")
                {
                    validInput = true;
                    UserMaintenance.UserMenu(currentSession);
                }
                else if (userInput == "no" || userInput == "n" || userInput == "change" || userInput == "different")
                {
                    validInput = true;
                    LogIn();
                }
            }
            while (validInput == false);
        }
        else
        {
            Console.WriteLine("No current user session found. Please log in again. Press any key to continue");
            //Console.ReadKey();
            LogIn();
        }
    }
    public static void LogIn()
    {
        //this method signs the user in after they key their email address
        bool nonUserTesting = false; //defunct, should be refactored out
        bool logInSuccess = false;
        string email = "";
        string password = "";
        Console.Clear();

        if (nonUserTesting == true)
        {
            Console.WriteLine("nonUserTesting flag is enabled");
            UserMaintenance.UserMenu(UserController.ReadCurrentUser());
        }
        else
        {
            do
            {
                UserInterface.WriteColors("Please enter your {=Green}email{/} to sign in to your account\n");
                email = Console.ReadLine().Trim();
                PassEmail:
                if (String.IsNullOrEmpty(email))
                {
                    Console.Clear();
                    UserInterface.WriteColors("{=Green}Email{/} cannot be blank. Please try again\n");
                }
                else if (!string.IsNullOrEmpty(email))
                {
                    if (UserController.FindUser(email) != null)
                    {
                        UserInterface.WriteColorsLine("Please key your {=Green}password{/} to complete sign-in");
                        password = Console.ReadLine().Trim();
                        User currentSession = UserController.FindUser(email);
                        PassPassword:
                        if (CryptoController.VerifyPassword(password,currentSession))
                            UserMaintenance.UserMenu(currentSession);
                        else
                        {
                            UserInterface.WriteColorsLine("{=Red}Incorrect password{/}. Please re-enter password or key \"exit\" to quit program");
                            password = Console.ReadLine().Trim();
                            if (password.ToLower() == "exit")
                                Environment.Exit(0);
                            else
                                goto PassPassword; //routes the user input back to the check for password validation
                        }
                    }
                    else
                    {
                        UserInterface.WriteColors("Email not found. Do you need to create an account? Re-enter your {=Green}email{/} or type {=Green}create{/} to make new account\n");
                        email = Console.ReadLine().Trim();
                        if (email.ToLower() == "create" || email.ToLower() == "c")
                            UserCreation.CreateUser();
                        else 
                            goto PassEmail; //routes the user input back to the email check
                    }
                }
            }
            while (logInSuccess == false);
        }
    }
}