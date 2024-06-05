namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IChecklistStorageEF
{
    public Task<List<Checklist>?> GetLists(User searchUser);    
    public void WriteChecklist(Checklist newList);

    public void WriteUpdatedList(Checklist updatedList);
    public void DeleteChecklist(Checklist deleteChecklist);
}