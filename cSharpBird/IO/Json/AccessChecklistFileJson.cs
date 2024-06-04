namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class AccessChecklistFileJson : IAccessChecklistFile
{
    public List<Checklist> GetLists(User searchUser)
    {
        //Will return the checklist list for the Checklists.json file, creating it and adding a default list if file does not already exist
        //string pathFile = "Checklists.json";
        string path = "data\\checklist";
        string pathFile = path + "\\Checklists.json";
        List<Checklist> ChecklistArchive = new List<Checklist>();
        List<Checklist> userChecklists = new List<Checklist>();
        string existingChecklistJSON;

        if (File.Exists(pathFile))
        {
            existingChecklistJSON =File.ReadAllText(pathFile);
            ChecklistArchive = JsonSerializer.Deserialize<List<Checklist>>(existingChecklistJSON);
        }
        else if(!File.Exists(pathFile))
        {
            Directory.CreateDirectory(path);
            ChecklistArchive.Add(new Checklist(searchUser.userId,"defaultLocation"));
            existingChecklistJSON = JsonSerializer.Serialize(ChecklistArchive);
            File.WriteAllText(pathFile,existingChecklistJSON);
        }
        //IEnumerable<Checklist> temp = ChecklistArchive.Where(i => i.userId == searchUser.userId);
        //userChecklists = temp.ToList();
        userChecklists = ChecklistArchive.Where(i => i.userId == searchUser.userId).ToList();
        return userChecklists;
    }
    public void WriteChecklist(Checklist newList)
    {
        //This method will write a new Checklist to the json file, creating it if it does not exist.
        //string pathFile = "Checklists.json";
        string path = "data\\checklist";
        string pathFile = path + "\\Checklists.json";
        List<Checklist> ChecklistArchive = new List<Checklist>();
        string existingChecklistJSON;
        if (File.Exists(pathFile))
        {
            existingChecklistJSON =File.ReadAllText(pathFile);
            ChecklistArchive = JsonSerializer.Deserialize<List<Checklist>>(existingChecklistJSON);
            ChecklistArchive.Add(newList);
            existingChecklistJSON = JsonSerializer.Serialize(ChecklistArchive);
            File.WriteAllText(pathFile,existingChecklistJSON);
        }
        else if(!File.Exists(pathFile))
        {
            Directory.CreateDirectory(path);
            ChecklistArchive.Add(newList); //newList.checklistID,"defaultLocation"));
            existingChecklistJSON = JsonSerializer.Serialize(ChecklistArchive);
            File.WriteAllText(pathFile,existingChecklistJSON);
        }
    }
    public void WriteUpdatedList(Checklist updatedList)
    {
        //This method will write an updated checklist to the json file, creating it if it does not exist.
        //string pathFile = "Checklists.json";
        string path = "data\\checklist";
        string pathFile = path + "\\Checklists.json";
        List<Checklist> ChecklistArchive = new List<Checklist>();
        string existingChecklistJSON;

        existingChecklistJSON =File.ReadAllText(pathFile);
        ChecklistArchive = JsonSerializer.Deserialize<List<Checklist>>(existingChecklistJSON);
        Checklist oldChecklist = ChecklistArchive.First(i => i.checklistID == updatedList.checklistID);
        var checklistLocation = ChecklistArchive.IndexOf(oldChecklist);
        if (checklistLocation != -1)
            ChecklistArchive[checklistLocation] = updatedList;
        else
            ChecklistArchive.Add(updatedList);
        
        existingChecklistJSON = JsonSerializer.Serialize(ChecklistArchive);
        File.WriteAllText(pathFile,existingChecklistJSON);
        BirdController.UpdateBirdsForChecklist(updatedList);
    }
    public void DeleteChecklist(Checklist deleteChecklist)
    {
        //This method will write an updated checklist to the json file, creating it if it does not exist.
        //string pathFile = "Checklists.json";
        string path = "data\\checklist";
        string pathFile = path + "\\Checklists.json";
        List<Checklist> ChecklistArchive = new List<Checklist>();
        string existingChecklistJSON;

        existingChecklistJSON =File.ReadAllText(pathFile);
        ChecklistArchive = JsonSerializer.Deserialize<List<Checklist>>(existingChecklistJSON);
        /*
        var checklistLocation = ChecklistArchive.IndexOf(deleteChecklist);
        if (checklistLocation != -1)
            ChecklistArchive.RemoveAt(checklistLocation);
        */
        Console.WriteLine($"ChecklistArchive contains {ChecklistArchive.Count}");
        ChecklistArchive.RemoveAll(i => i.checklistID == deleteChecklist.checklistID);
        List<Checklist> ChecklistRemoved = ChecklistArchive;
        Console.WriteLine($"ChecklistArchive now contains {ChecklistRemoved.Count}");
        existingChecklistJSON = JsonSerializer.Serialize(ChecklistRemoved);
        File.WriteAllText(pathFile,existingChecklistJSON);
        BirdController.DeleteBirdsForChecklist(deleteChecklist);
    }
}