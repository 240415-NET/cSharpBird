namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IBirdStorageEF
{
    public Task<List<Bird>?> GetFullBirdList();
    public void WriteBirdsForChecklist(Checklist checklist);
    public void UpdateBirdsForChecklist(Checklist checklist);
    public Task<List<Bird>?> ReadBirdsForChecklist(Guid checklistID);
    public void DeleteBirdsForChecklist(Checklist checklist);
}