namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class UserInterface
{
    public static void menuPrintBase(string[] args)
    {
        //calls submethods appropriately for the arguments passed
        string[] initialPrompt = args;
        UserInterface.menuFillVertical(initialPrompt);
        UserInterface.menuFillHorizontal(initialPrompt);
        UserInterface.menuFillVertical(initialPrompt);
    }
    public static void menuFillVertical(string[] args)
    {
        //creates vertical spacing appropriate for the number of menu items in array passed
        int wConsole = Console.WindowWidth;
        int hConsole = Console.WindowHeight;
        string[] menuPrint = args;

        for (int h = 0; h < (hConsole - menuPrint.Length - 2)/2; h++)
        {
            menuFillHorizontalEmpty();
            Console.Write('\n');
        }
    }
    public static void menuFillHorizontalEmpty()
    {
        int wConsole = Console.WindowWidth;
        for (int w = 0; w < wConsole; w++)
            Console.Write("=");
    }
    public static void menuFillHorizontal(string[] args)
    {
        //prints string array for menu centered in columns
        int wConsole = Console.WindowWidth;
        int hConsole = Console.WindowHeight;
        string[] menuPrint = args;

        for (int s = 0; s < menuPrint.Length; s++)
        {
            Console.Write("===");
            if (ColorLength(menuPrint[s]) % 2 == 0)
            {
                for (int w = 0; w < (wConsole - (ColorLength(menuPrint[s]) + 6))/2; w++)
                {
                    Console.Write(" ");
                }
                UserInterface.WriteColors(menuPrint[s]);
                for (int w = 0; w < ((wConsole - (ColorLength(menuPrint[s]) + 6))/2)-1; w++)
                {
                    Console.Write(" ");
                }
            }
            else
            {
                for (int w = 0; w < ((wConsole - (ColorLength(menuPrint[s]) + 6))/2); w++)
                {
                    Console.Write(" ");
                }
                UserInterface.WriteColors(menuPrint[s]);
                for (int w = 0; w < ((wConsole - (ColorLength(menuPrint[s]) + 6))/2); w++)
                {
                    Console.Write(" ");
                }
            }
            Console.Write(" ===");
            Console.Write('\n');
        }
    }
    public static string exitChecker(string exitCheck)
    {  
        //this will either return the same string or begin the exit checker process as needed
        exitCheck = exitCheck.Trim();
        if (exitCheck.ToLower() == "q" || exitCheck.ToLower() == "quit")
        {
            exitConfirm();
        }
        return exitCheck;
    }
    public static void exitConfirm()
    {
        //This is a public method to confirm exit when processes have begun in either employee or shopper modes
        Console.WriteLine("Are you sure? Data will not be saved! Confirm by typing 'g'");
        if (Console.ReadLine().ToLower() == "g")
        {
            Console.WriteLine("Exiting. Goodbye!");
            Environment.Exit(0);
        }
        else
            UserMaintenance.UserMenu(UserController.ReadCurrentUser());
    }
    public static void exit()
    {
        //Simplified exit method for use when there is no data being handled
        Console.WriteLine("Are you sure? Confirm by typing 'g'");
        if (Console.ReadLine().ToLower() == "g")
        {
            Console.WriteLine("Exiting. Goodbye!");
            Environment.Exit(0);
        }
        else
            UserMaintenance.UserMenu(UserController.ReadCurrentUser());
    }
    public static void logoff()
    {
        //Simplified exit method for use when there is no data being handled
        Console.WriteLine("Are you sure? Confirm by typing 'g'");
        if (Console.ReadLine().ToLower() == "g")
        {
            UserController.ClearCurrentUser();
            Console.WriteLine("You're now logged out. Goodbye!");
            Environment.Exit(0);
        }
        else
            UserMaintenance.UserMenu(UserController.ReadCurrentUser());
    }
    public static void WriteColors(string msg)
    {
        //Use UserInterface.WriteColors to write colored text in the console using {=Color}Example{/} to format
        string[] ss = msg.Split('{','}');
        ConsoleColor c;
        foreach(var s in ss)
            if(s.StartsWith("/"))
                Console.ResetColor();
            else if(s.StartsWith("=") && Enum.TryParse(s.Substring(1), out c))
                Console.ForegroundColor = c;
            else
                Console.Write(s);
    }
    public static void WriteColorsLine(string msg)
    {
        //Use UserInterface.WriteColors to write colored text in the console using {=Color}Example{/} to format and adds a line break
        string[] ss = msg.Split('{','}');
        ConsoleColor c;
        foreach(var s in ss)
            if(s.StartsWith("/"))
                Console.ResetColor();
            else if(s.StartsWith("=") && Enum.TryParse(s.Substring(1), out c))
                Console.ForegroundColor = c;
            else
                Console.Write(s);
        Console.Write("\n");
    }
    public static int ColorLength(string msg)
    {
        //Use UserInterface.ColorLength to determine the length of colored text formatted for UserInterface.WriteColors
        string[] ss = msg.Split('{','}');
        ConsoleColor c;
        int ColorLength = 0;
        foreach(var s in ss)
            if(s.StartsWith("/"))
                Console.ResetColor();
            else if(s.StartsWith("=") && Enum.TryParse(s.Substring(1), out c))
                Console.ForegroundColor = c;
            else
                ColorLength += s.Length;
            return ColorLength;
    }
}