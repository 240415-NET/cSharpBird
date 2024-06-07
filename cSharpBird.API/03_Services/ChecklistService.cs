using Microsoft.Identity.Client;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cSharpBird.API;

public class ChecklistService : IChecklistService
{
    private readonly IChecklistStorageEF _checklistStorage;
    public ChecklistService (IChecklistStorageEF efRepoFromBuilder)
    {
        _checklistStorage = efRepoFromBuilder;
    }
    public async Task<Checklist> CreateNewChecklistAsync (Checklist newChecklist)
    {

    }
    public async Task<List<Checklist>> GetChecklistsAsync (User user)
    {
        return await _checklistStorage.GetListsAsync(user);
    }
    public async Task<Checklist> WriteChecklistAsync (Checklist newList)
    {
        return await _checklistStorage.WriteChecklist(newList);
    }
    public async Task<Checklist> WriteUpdatedListAsync(Checklist updatedChecklist)
    {
        return await _checklistStorage.WriteUpdatedListAsync(updatedChecklist);
    }
    public async Task<bool> DeleteChecklistAsync (Checklist deleteChecklist)
    {
        return await _checklistStorage.DeleteChecklistAsync(deleteChecklist);
    }
}