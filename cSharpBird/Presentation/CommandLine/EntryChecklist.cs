namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class EntryChecklist
{
    public static void Menu()
    {
        //root checklist menu to manage, edit and delete checklists
        Console.Clear();
        string[] menu = { "{=Magenta}CHECKLIST MENU{/}", "{=Green}1. Create{/} New Checklist", "{=Cyan}2. List{/} Existing Checklists", "{=Blue}3. Edit or Print{/} Existing Checklist Details", "{=Red}4. Exit{/} to Menu" };
        string menuRequest;
        bool validInput = false;
        UserInterface.menuPrintBase(menu);
        do
        {
            try
            {
                menuRequest = Console.ReadLine();
                switch (menuRequest.ToLower())
                {
                    case "1":
                    case "1.":
                    case "1. create":
                    case "new":
                    case "create new":
                        validInput = true;
                        EntryChecklist.Create();
                        break;
                    case "2":
                    case "2.":
                    case "2. list":
                    case "list":
                    case "list all":
                    case "view":
                    case "view all":
                        validInput = true;
                        EntryChecklist.List(UserController.ReadCurrentUser());
                        break;
                    case "3":
                    case "3.":
                    case "3. edit":
                    case "edit":
                    case "edit checklist":
                    case "details":
                        validInput = true;
                        EntryChecklist.Edit(UserController.ReadCurrentUser());
                        break;
                    case "4":
                    case "4.":
                    case "4. exit":
                    case "exit":
                    case "quit":
                    case "back":
                    case "cancel":
                        validInput = true;
                        UserMaintenance.UserMenu(UserController.ReadCurrentUser());
                        break;
                    default:
                        Console.WriteLine("Please enter valid selection");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}. Please key in valid selection for Checklist home menu");
            }
        }
        while (validInput == false);
    }
    public static void Create()
    {
        //basic method for creating new checklist
        string hotspot;
        string checklistDate;
        string[] createMenu = { "{=Magenta}NEW CHECKLIST{/}", "{=Green}1. Current{/} Checklist", "{=Blue}2. Historic{/} Checklist", "{=Red}3. Cancel{/}" };
        bool validInput = false;
        string menuRequest;
        User currentUser = UserController.ReadCurrentUser();

        Console.Clear();

        UserInterface.menuPrintBase(createMenu);
        do
        {
            try
            {
                menuRequest = Console.ReadLine();
                switch (menuRequest.ToLower())
                {
                    case "1":
                    case "1.":
                    case "1. current":
                    case "current":
                    case "create new":
                    case "today":
                    case "now":
                        validInput = true;
                        collectInitData();
                        break;
                    case "2":
                    case "2.":
                    case "2. historic":
                    case "past":
                    case "historic":
                        validInput = true;
                        collectInitDataHistoric();
                        break;
                    case "3":
                    case "3.":
                    case "3. exit":
                    case "exit":
                    case "quit":
                    case "back":
                    case "cancel":
                        validInput = true;
                        Menu();
                        break;
                    default:
                        Console.WriteLine("Please enter valid selection");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}. Please key in valid selection for Create menu");
            }
        }
        while (validInput == false);
    }
    public static void List(User user)
    {
        //lists checklists from current user
        Console.Clear();
        List<Checklist> userChecklists = new List<Checklist>();
        userChecklists = ChecklistController.GetLists(user);

        string userInput;
        bool validInput = false;

        int userSelect = 0;
        int listNum = 0;

        UIChecklist.ListLists(userChecklists);

        try
        {
            do
            {
                userInput = UserInterface.exitChecker(Console.ReadLine().Trim());
                userSelect = Convert.ToInt32(userInput);
                if (userSelect == 0)
                {
                    validInput = true;
                    Menu();
                }
                else if (userSelect <= userChecklists.Count())
                {
                    validInput = true;
                    ViewAndAppend(userChecklists[userSelect - 1]);
                }
                else
                    Console.WriteLine("Please key in valid checklist number");
            }
            while (validInput == false);
        }
        catch (Exception o)
        {
            Console.WriteLine("Please enter valid checklist number");
            Console.WriteLine(o.StackTrace);
            Console.WriteLine(o.Message);
        }
    }
    public static void collectInitData()
    {
        //collects initial data for checklist, calls to write object to file and routes to ViewAndAppend to add sightings
        User currentUser = UserController.ReadCurrentUser();
        Console.Clear();
        Console.WriteLine("What hotspot should be used for this checklist?");
        string hotspot = Console.ReadLine().Trim();
        try
        {
            Checklist checklist = new Checklist(currentUser.userId, hotspot);
            checklist.birds = BirdController.GetFullBirdList();
            ChecklistController.WriteChecklist(checklist);
            ViewAndAppend(checklist);
        }
        catch (Exception i)
        {
            Console.WriteLine(i.StackTrace);
            Console.WriteLine(i.Message);
        }
    }
    public static void collectInitDataHistoric()
    {
        //collects initial data for historic checklist, calls to write object to file and routes to ViewAndAppend to add sightings
        User currentUser = UserController.ReadCurrentUser();
        Console.Clear();
        Console.WriteLine("When was this checklist taken?");
        string checklistDate = Console.ReadLine().Trim();
        Console.WriteLine("What hotspot should be used for this checklist?");
        string hotspot = Console.ReadLine().Trim();
        try
        {
            Checklist historicChecklist = new Checklist(currentUser.userId, hotspot, checklistDate);
            historicChecklist.birds = BirdController.GetFullBirdList();
            ChecklistController.WriteChecklist(historicChecklist);
            ViewAndAppend(historicChecklist);
        }
        catch (Exception ih)
        {
            Console.WriteLine(ih.StackTrace);
            Console.WriteLine(ih.Message);
        }
    }
    public static void ViewAndAppend(Checklist viewList)
    {
        //this method shows the basics of the given checklist object and allows the user to view the full species sighted list and add new ones
        List<Bird> loggedBirds = BirdController.ReadBirdsForChecklist(viewList.checklistID);
        viewList.birds = loggedBirds.ToList();
        bool userDone = false;
        string userInput = "";
        do
        {
            UIChecklist.viewList(viewList);
            UserInterface.WriteColorsLine("Please key a {=Green}band code{/} and the {=Blue}number{/} of that bird seen to log or {=Red}done{/} to finish list");
            try
            {
                userInput = UserInterface.exitChecker(Console.ReadLine().Trim());
                if (userInput.ToLower() == "done" || userInput.ToLower() == "d")
                {
                    userDone = true;
                    if (viewList.duration == 0)
                    {
                        bool validDuration = false;
                        UserInterface.WriteColorsLine("What was the {=Green}duration in minutes{/} for this checklist?");
                        do
                        {
                            try
                            {
                                viewList.duration = int.Parse(Console.ReadLine().Trim());
                                validDuration = true;
                            }
                            catch (System.Exception)
                            {
                                UserInterface.WriteColorsLine("{=Red}Not a valid input.{/} Please key a duration in minutes for this checklist");
                            }
                        } while (validDuration == false);
                    }
                    if (viewList.distance == 0 && viewList.stationary == false)
                    {
                        bool validDistance = false;
                        UserInterface.WriteColorsLine("What was the {=Green}distance in miles{/} travelled for this checklist?");
                        do
                        {
                            try
                            {
                                viewList.distance = float.Parse(Console.ReadLine().Trim());
                                if (viewList.distance == 0)
                                    viewList.stationary = true;
                                validDistance = true;
                            }
                            catch (System.Exception)
                            {
                                UserInterface.WriteColorsLine("{=Red}Not a valid input.{/} Please key a distance in miles for this checklist");
                            }
                        } while (validDistance == false);
                    }
                }
                else if (ChecklistController.ValidListUpdate(userInput))
                {
                    ChecklistController.ListUpdate(userInput, viewList);
                    Console.Clear();
                }
                else
                {
                    UserInterface.WriteColorsLine("Please key a {=Green}band code{/} and the {=Blue}number{/} of that bird seen to log separated by a space. Key {=Green}band code{/} and '0' to remove\nPress any key to continue");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }
        while (userDone == false);
        ChecklistController.WriteUpdatedList(viewList);
        Menu();
    }
    public static void Edit(User user)
    {
        //shows user's checklists for editing details
        Console.Clear();
        List<Checklist> userChecklists = new List<Checklist>();
        userChecklists = ChecklistController.GetLists(user);

        string userInput;
        bool validInput = false;

        int userSelect = 0;
        int listNum = 0;

        UIChecklist.ListEdits(userChecklists);
        do
        {
            try
            {
                userInput = UserInterface.exitChecker(Console.ReadLine().Trim());
                userSelect = Convert.ToInt32(userInput);
                if (userSelect == 0)
                {
                    validInput = true;
                    Menu();
                }
                else if (userSelect <= userChecklists.Count())
                {
                    validInput = true;
                    SelectedEdit(userChecklists[userSelect - 1]);
                }
                else
                    Console.WriteLine("Please key in valid checklist number");
            }
            catch (Exception o)
            {
                Console.WriteLine("Please enter valid checklist number");
            }
        }
        while (validInput == false);
    }
    public static void SelectedEdit(Checklist oldChecklist)
    {
        //prints and prompts for edits to selected checklist details
        string editRequest;
        bool validInput = false;
        Console.Clear();
        string[] menu = { "What would you like to change on this list today?", "{=Green}1. Location{/}: " + oldChecklist.locationName, "{=Cyan}2. Date{/}: " + oldChecklist.checklistDateTime.ToString("d"), "{=Blue}3. Species{/}: " + ChecklistController.CountListBird(oldChecklist), "{=DarkGreen}4. Print{/}", "{=Yellow}5. Return{/}", "{=Red}6. Delete{/} this checklist" };

        UserInterface.menuPrintBase(menu);
        do
        {
            try
            {
                editRequest = Console.ReadLine();
                switch (editRequest.ToLower())
                {
                    case "1":
                    case "1.":
                    case "1. location":
                    case "location":
                        validInput = true;
                        changeLocation(oldChecklist);
                        break;
                    case "2":
                    case "2.":
                    case "2. date":
                    case "date":
                        validInput = true;
                        changeDate(oldChecklist);
                        break;
                    case "3":
                    case "3.":
                    case "3. species":
                    case "species":
                    case "birds":
                    case "bird":
                        validInput = true;
                        ViewAndAppend(oldChecklist);
                        break;
                    case "4":
                    case "4.":
                    case "4. print":
                    case "print":
                    case "text":
                    case "save":
                        validInput = true;
                        Print(oldChecklist);
                        break;
                    case "5":
                    case "5.":
                    case "5. cancel":
                    case "cancel":
                    case "exit":
                    case "quit":
                    case "return":
                    case "back":
                        validInput = true;
                        Edit(UserController.ReadCurrentUser());
                        break;
                    case "6":
                    case "6.":
                    case "6. delete":
                    case "delete":
                        validInput = true;
                        Delete(oldChecklist);
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
        while (validInput == false);
    }
    public static void Delete(Checklist oldChecklist)
    {
        //allows for deletion of a given checklist
        Console.Clear();
        string prompt = "{=Red}Are you sure you wish to delete this checklist? This can NOT be undone!{/}\nKey {=Red}\"confirm\"{/} to delete or {=Green}back{/} to return to prior menu.";
        string userInput = "";
        bool validInput = false;

        UserInterface.WriteColorsLine(prompt);

        do
        {
            userInput = Console.ReadLine().Trim();
            switch (userInput.ToLower())
            {
                case "0":
                case "back":
                case "done":
                case "return":
                case "cancel":
                case "keep":
                    validInput = true;
                    SelectedEdit(oldChecklist);
                    break;
                case "confirm":
                case "delete":
                    validInput = true;
                    ChecklistController.DeleteChecklist(oldChecklist);
                    prompt = "{=Red}Checklist has been deleted. Press any key to continue";
                    UserInterface.WriteColorsLine(prompt);
                    Console.ReadKey();
                    Edit(UserController.ReadCurrentUser());
                    break;
                default:
                    Console.WriteLine("Please key as instructed above.");
                    break;
            }
        }
        while (validInput == false);
    }
    public static void Print(Checklist oldChecklist)
    {
        //prints a checklist to a text file for a portable version
        Console.Clear();
        UserInterface.WriteColorsLine("Please enter a filepath and name to save this checklist to, or leave blank to save to default location");
        string userInput = Console.ReadLine().Trim();
        ChecklistController.Print(userInput, oldChecklist);
        SelectedEdit(oldChecklist);
    }
    public static void changeLocation(Checklist oldChecklist)
    {
        //allows for user to change checklist location
        Console.Clear();
        string descriptor = "{=Green}" + oldChecklist.locationName + "{/} is the current location. Please key a new location or 0 to return to previous menu";
        UserInterface.WriteColorsLine(descriptor);
        string userInput = Console.ReadLine().Trim();
        try
        {
            switch (userInput.ToLower())
            {
                case "0":
                case "back":
                case "done":
                case "return":
                    SelectedEdit(oldChecklist);
                    break;
                default:
                    List<Bird> loggedBirds = BirdController.ReadBirdsForChecklist(oldChecklist.checklistID);
                    oldChecklist.birds = loggedBirds.ToList();
                    ChecklistController.LocationUpdate(userInput, oldChecklist);
                    descriptor = "Checklist location updated to {=Green}" + userInput + "{/}";
                    UserInterface.WriteColorsLine(descriptor);
                    Console.ReadKey();
                    SelectedEdit(oldChecklist);
                    break;
            }
        }
        catch (Exception cl)
        {
            Console.WriteLine(cl.StackTrace);
            Console.WriteLine(cl.Message);
        }
    }
    public static void changeDate(Checklist oldChecklist)
    {
        //allows for user ot change checklist date
        //needs handling for invalid input still
        Console.Clear();
        string descriptor = "{=Green}" + oldChecklist.checklistDateTime + "{/} is the current date. Please key a new date or 0 to return to previous menu";
        UserInterface.WriteColorsLine(descriptor);
        string userInput = Console.ReadLine().Trim();
        switch (userInput.ToLower())
        {
            case "0":
            case "back":
            case "done":
            case "return":
                SelectedEdit(oldChecklist);
                break;
            default:
                List<Bird> loggedBirds = BirdController.ReadBirdsForChecklist(oldChecklist.checklistID);
                oldChecklist.birds = loggedBirds.ToList();
                ChecklistController.DateUpdate(userInput, oldChecklist);
                descriptor = "Checklist date updated to {=Green}" + userInput + "{/}. Press any key to continue.";
                UserInterface.WriteColorsLine(descriptor);
                Console.ReadKey();
                SelectedEdit(oldChecklist);
                break;
        }
    }
}