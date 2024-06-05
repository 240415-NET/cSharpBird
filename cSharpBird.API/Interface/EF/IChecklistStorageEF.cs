namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IChecklistStorageEF
{
    public Task<List<Checklist>?> GetLists(User searchUser);    
    public Task<Checklist> WriteChecklist(Checklist newList);

    public Task<Checklist> WriteUpdatedList(Checklist updatedList);
    public Task<bool> DeleteChecklist(Checklist deleteChecklist);
}