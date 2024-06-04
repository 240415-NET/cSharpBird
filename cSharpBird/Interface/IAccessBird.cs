namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IAccessBird
{
    public List<Bird> GetFullBirdList();
    public void WriteBirdsForChecklist(Checklist checklist);
    public void UpdateBirdsForChecklist(Checklist checklist);
    public List<Bird> ReadBirdsForChecklist(Guid checklistID);
    public void DeleteBirdsForChecklist(Checklist checklist);
}