namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class ChecklistController
{
    public static IAccessChecklistFile AccessChecklistFile = new ChecklistSQL();
    public static List<Checklist> GetLists(User searchUser)
    {
        //calls interface to get lists for current user
        List<Checklist> userChecklists = AccessChecklistFile.GetLists(searchUser);
        return userChecklists;
    }
    public static void WriteChecklist(Checklist newList)
    {
        //calls interface to write checklist to file
        AccessChecklistFile.WriteChecklist(newList);
        //BirdController.WriteBirdsForChecklist(newList);
    }
    public static void WriteUpdatedList(Checklist updatedList)
    {
        //calls interface to write updated checklist to file
        AccessChecklistFile.WriteUpdatedList(updatedList);
        //BirdController.WriteBirdsForChecklist(updatedList);
    }
    public static List<Bird> RetrieveLoggedBirds(Checklist checklist)
    {
        //calls interface in BirdController to return just to list of species from checklist
        List<Bird> birdList = BirdController.ReadBirdsForChecklist(checklist.checklistID);
        //this lambda expression resaves list of species as just those that have a number greater than 0
        birdList = birdList.Where(i => i.numSeen > 0).ToList();
        return birdList;
    }
    public static bool ValidListUpdate(string userInput)
    {
        //this method validates the data that was given by the user to add species to the checklist
        userInput = userInput.Trim();
        string[] input = userInput.Split(' ',',');
        bool valid = false;
        //confirms that there are two substrings, one for a given species and one for a valid integer
        if (input[0].Length == 4 && Convert.ToInt64(input[1]) > -1)
            valid = true;
        if  (userInput.Length <= 4)
            valid = false;
        return valid;
    }
    public static void ListUpdate(string userInput,Checklist checklist)
    {
        //method will update the list of birds within the checklist and write an updated checklist to file
        userInput = userInput.Trim();
        string[] input = userInput.Split(' ',',');
        bool valid = false;
        //locates the specified bandcode from the user
        Bird updatedCount = checklist.birds.First(i => i.bandCode == input[0].ToUpper());
        var checklistLocation = checklist.birds.IndexOf(updatedCount);
        updatedCount.numSeen = int.Parse(input[1]);
        //if the location is valid (i.e. a valid band code), updates the proper record
        if (checklistLocation != -1)
            checklist.birds[checklistLocation] = updatedCount;
        AccessChecklistFile.WriteUpdatedList(checklist);
        //BirdController.WriteBirdsForChecklist(checklist);
    }
    public static List<Bird> PrintListBird(Checklist checklist)
    {
        //simply gives the list of birds sighted for printing
        List<Bird> birdList = BirdController.ReadBirdsForChecklist(checklist.checklistID);
        birdList = birdList.Where(i => i.numSeen > 0).ToList();
        return birdList;
    }
    public static int CountListBird(Checklist checklist)
    {
        //returns the count of total species seen
        List<Bird> birdList = BirdController.ReadBirdsForChecklist(checklist.checklistID);
        birdList = birdList.Where(i => i.numSeen > 0).ToList();
        int count = birdList.Count();
        return count;
    }
    public static void LocationUpdate(string userInput,Checklist checklist)
    {
        //calls interface to update location
        checklist.locationName = userInput;
        AccessChecklistFile.WriteUpdatedList(checklist);
    }
    public static void DateUpdate(string userInput,Checklist checklist)
    {
        //calls interface to update date
        checklist.checklistDateTime = DateTime.Parse(userInput);
        AccessChecklistFile.WriteUpdatedList(checklist);
    }
    public static void DeleteChecklist(Checklist checklist)
    {
        //calls interface to delete unneeded list
        AccessChecklistFile.DeleteChecklist(checklist);
    }
    public static void Print(string userInput,Checklist checklist)
    {
        //called to print checklist ot a text file
        string[] headers = {"Number","Location","Count"};
        string headerLine = string.Format("{0,-20} {1,-35} {2,-15}",headers[0],headers[1],headers[2]);
        string prettyPrint = "Location: " + checklist.locationName + "\nDate: " + checklist.checklistDateTime.ToString("d") +  "\n" + headerLine;
        string pathFile = "";
        int i = 0;
        string printLine = "";

        //gets current user for file name
        User user = UserController.ReadCurrentUser();
        //determines what data to use in file name
        if (userInput == null || userInput == "")
        {
            if (user.displayName == null)
                pathFile = user.userName + "_" + checklist.locationName + "_" + checklist.checklistDateTime.ToString("MMddyyyy") + ".txt";
            else
                pathFile = user.displayName + "_" + checklist.locationName + "_" + checklist.checklistDateTime.ToString("MMddyyyy") + ".txt";
        }
        else
            pathFile = userInput; //for user-specified location and name

        

        List<Bird> loggedBirds = ChecklistController.PrintListBird(checklist); //retrieves list of birds that are >0
        if (loggedBirds.Count() > 0)
        {
            using (StreamWriter outputFile = new StreamWriter(pathFile))
            {
                outputFile.WriteLine(prettyPrint); //writes headers
                foreach (Bird b in loggedBirds)
                {
                    printLine = string.Format("{0,-15} {1,-35} {2,15}",loggedBirds[i].bandCode,loggedBirds[i].speciesName,loggedBirds[i].numSeen);
                    outputFile.WriteLine(printLine);
                    i++;
                }
            }
            //UserInterface.WriteColorsLine("{=Green}File successfully saved!{/} Your checklist is here:\n"+pathFile+"\nPress any key to return to prior menu");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            //UserInterface.WriteColorsLine("{=Red}File not saved!{/} Please add some birds to print your checklist. Press any key to add birds.");
            Console.ReadKey();
            Console.Clear();
            //EntryChecklist.ViewAndAppend(checklist);
        }
    }
}