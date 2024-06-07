namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IChecklistStorageEF
{
    public Task<List<Checklist?>?> GetListsAsync(Guid searchUser);    
    public Task<Checklist> WriteChecklistAsync(Checklist newList);

    public Task<Checklist> WriteUpdatedListAsync(Checklist updatedList);
    public Task<bool> DeleteChecklistAsync(Checklist deleteChecklist);
}