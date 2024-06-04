namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class UIChecklist
{
    public static void viewList(Checklist xlist)
    {
        //calls submethods appropriately for the arguments passed
        //prints items as needed from existing checklist items
        Console.Clear();
        UIChecklist.PrintHeader(xlist);
        List<Bird> loggedBirds = ChecklistController.PrintListBird(xlist);
        if (loggedBirds.Count() > 0)
            PrintLoggedBirds(loggedBirds);
    }
    public static void PrintHeader(Checklist xlist)
    {
        //this method prints the header for the checklist view
        User currentUser = UserController.ReadCurrentUser();
        List<Bird> loggedBirds = ChecklistController.PrintListBird(xlist);
        string tempName;
        try
        {
            if (currentUser.displayName == null || currentUser.displayName == "" || currentUser.displayName == "NULL")
                tempName = currentUser.userName;
            else
                tempName = currentUser.displayName;
            UserInterface.WriteColorsLine("{=Magenta}" + tempName + "'s " + xlist.locationName + " checklist for " + xlist.checklistDateTime.ToString("d") + "{/}");
            if (loggedBirds.Count() == 0)
                UserInterface.WriteColorsLine("{=Red}No birds logged yet{/}");
            else
                UserInterface.WriteColorsLine("{=Blue}Species logged: " + loggedBirds.Count() +"{/}");
            UserInterface.menuFillHorizontalEmpty();
        }
        catch (Exception l)
        {
            Console.WriteLine(l.Message);
        }
    }
    public static void PrintLoggedBirds(List<Bird> loggedBirds)
    {
        //this method loops through and prints the species with one or more individuals sighted
        int i = 0;
        string[] headers = {"{=Green}Band Code{/}","{=Cyan}Species Name{/}","{=Blue}Number Seen{/}"};
        string[] birdData;
        string printLine = string.Format("{0,-20}\t{1,-35}\t{2,15}",headers[0],headers[1],headers[2]);
        try
        {
            UserInterface.WriteColorsLine(printLine);
            foreach (Bird b in loggedBirds)
            {
                printLine = string.Format("{0,-20}{1,-35}{2,15}",loggedBirds[i].bandCode,loggedBirds[i].speciesName,loggedBirds[i].numSeen);
                UserInterface.WriteColorsLine(printLine);
                i++;
            }
        }
        catch (Exception p)
        {
            Console.WriteLine(p.StackTrace);
            Console.WriteLine(p.Message);
            Console.ReadKey();
        }

    }
    public static void ListLists(List<Checklist> userChecklists)
    {
        //lists the list of checklists and a header for it
        Console.Clear();
        string[] headers = {"{=Blue}Number{/}","Location","Date"};
        string headerLine = string.Format("{0,-20} {1,-35} {2,-15}",headers[0],headers[1],headers[2]);
        string[] listDetails = new string[3];
        string listLine = "";
        int listNum = 0;
        
        UserInterface.WriteColorsLine("Please enter the {=Blue}Checklist number{/} you'd like to view additional details for, or 0 to go back");
        UserInterface.WriteColorsLine(headerLine);
        for (int i = 0; i < userChecklists.Count(); i++)
        {
            listNum = i+1;
            listDetails[0] = "{=Blue}" + listNum + "{/}";
            listDetails[1] = userChecklists[i].locationName;
            listDetails[2] = userChecklists[i].checklistDateTime.ToString("d");
            listLine = string.Format("{0,-20} {1,-35} {2,-15}",listDetails[0],listDetails[1],listDetails[2]);
            UserInterface.WriteColorsLine(listLine);
        }
    }
    public static void ListEdits(List<Checklist> userChecklists)
    {
        //lists lists of checklists and prompts for selection to edit
        Console.Clear();
        string[] headers = {"{=Blue}Number{/}","Location","Date"};
        string headerLine = string.Format("{0,-20} {1,-35} {2,-15}",headers[0],headers[1],headers[2]);
        string[] listDetails = new string[3];
        string listLine = "";
        int listNum = 0;
        
        UserInterface.WriteColorsLine("Please enter the {=Blue}Checklist number{/} you'd like to print or change details for, or 0 to go back");
        UserInterface.WriteColorsLine(headerLine);
        for (int i = 0; i < userChecklists.Count(); i++)
        {
            listNum = i+1;
            listDetails[0] = "{=Blue}" + listNum + "{/}";
            listDetails[1] = userChecklists[i].locationName;
            listDetails[2] = userChecklists[i].checklistDateTime.ToString("d");
            listLine = string.Format("{0,-20} {1,-35} {2,-15}",listDetails[0],listDetails[1],listDetails[2]);
            UserInterface.WriteColorsLine(listLine);
        }
    }
}