namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IBirdStorageEF
{
    //public List<Bird> GetFullBirdList();
    public Task<Checklist> WriteBirdsForChecklist(Checklist checklist);
    public Task<Checklist> UpdateBirdsForChecklist(Checklist checklist);
    public Task<List<Bird>?> ReadBirdsForChecklist(Guid checklistID);
    public Task<Checklist> DeleteBirdsForChecklist(Checklist checklist);
}