namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class AccessBirdCSV : IAccessBird
{
    public List<Bird> GetFullBirdList()
    {
        //pulls full bird species data from a given csv. two files are currently present; USGSBBL.csv for the USGS Bird Banding Laboratory codes and ABACL for American Birding Association codes.
        //update line 15 to change standards
        string bandCode = "";
        string speciesName = "";
        string path = "data\\BirdCSV\\";
        string pathFile = path + "USGSBBL.csv";

        List<Bird> birdList = new List<Bird>();
        try
        {
            birdList = File.ReadAllLines(pathFile)
                .Select(line => line.Split(','))
                .Select(x => new Bird{
                    bandCode = x[0],
                    speciesName = x[1]
                }).ToList();
        }
        catch (Exception b)
        {
            Console.WriteLine(b.StackTrace);
            Console.WriteLine(b.Message);
        }

        return birdList;
    }
    public void WriteBirdsForChecklist (Checklist checklist)
    {
        //writes checklist to file in subdirectory
        string path = "data\\checklist\\" + checklist.checklistID;
        string pathFile = path + "\\birds.json";
        if (File.Exists(pathFile))
        {
            string existingChecklistJSON = JsonSerializer.Serialize(checklist.birds);
            File.WriteAllText(pathFile,existingChecklistJSON);
        }
        else if (!File.Exists(pathFile))
        {
            Directory.CreateDirectory(path);
            string existingChecklistJSON = JsonSerializer.Serialize(checklist.birds);
            File.WriteAllText(pathFile,existingChecklistJSON);
        }
    }
    public void UpdateBirdsForChecklist (Checklist checklist)
    {
        //writes birds updated in checklist
        WriteBirdsForChecklist(checklist);
    }
    public List<Bird> ReadBirdsForChecklist (Guid checklistID)
    {
        //reads birds to list from subdirectory
        string path = "data\\checklist\\" + checklistID;
        string pathFile = path + "\\birds.json";
        List<Bird> birdList = new List<Bird>();
        if (File.Exists(pathFile))
        {
            string existingChecklistJSON = File.ReadAllText(pathFile);
            birdList = JsonSerializer.Deserialize<List<Bird>>(existingChecklistJSON);
        }
        else if (!File.Exists(pathFile))
        {
            Directory.CreateDirectory(path);
            string existingChecklistJSON = JsonSerializer.Serialize(GetFullBirdList());
            File.WriteAllText(pathFile,existingChecklistJSON);
            existingChecklistJSON = File.ReadAllText(pathFile);
            birdList = JsonSerializer.Deserialize<List<Bird>>(existingChecklistJSON);
        }
        return birdList;
    }
    public void DeleteBirdsForChecklist(Checklist deleteChecklist)
    {
        string path = "data\\checklist\\" + deleteChecklist.checklistID;
        string pathFile = path + "\\birds.json";
        System.IO.DirectoryInfo dPath = new System.IO.DirectoryInfo(path);
        if (File.Exists(pathFile))
        {
            foreach (FileInfo file in dPath.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in dPath.GetDirectories())
                dir.Delete();
        }
    }
}