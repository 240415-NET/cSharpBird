namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
public class BirdController
{
    public static IAccessBird AccessBird = new BirdSQL();
    public static List<Bird> GetFullBirdList()
    {
        //for checklist file creation, calls interface to retrieve for csv
        List<Bird> birdList = AccessBird.GetFullBirdList();
        return birdList;
    }
    public static void WriteBirdsForChecklist (Checklist checklist)
    {
        //Needed for SQL
        AccessBird.WriteBirdsForChecklist(checklist);
    }
    public static void UpdateBirdsForChecklist (Checklist checklist)
    {
        //Needed for SQL
        AccessBird.UpdateBirdsForChecklist(checklist);
    }
    public static List<Bird> ReadBirdsForChecklist (Guid checklistID)
    {
        //Needed for SQL
        List<Bird> birdList = AccessBird.ReadBirdsForChecklist(checklistID);
        return birdList;
    }
    public static void DeleteBirdsForChecklist (Checklist checklist)
    {
        AccessBird.DeleteBirdsForChecklist(checklist);
    }
}