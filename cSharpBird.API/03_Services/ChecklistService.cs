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
    private readonly IBirdStorageEF _birdStorage;
    public ChecklistService (IChecklistStorageEF efRepoFromBuilder, IBirdStorageEF efRepoFromBuilder2)
    {
        _checklistStorage = efRepoFromBuilder;
        _birdStorage = efRepoFromBuilder2;
    }
    //This may be redundant if we can continue to pull in the list of birds via the CSV file
    public async Task<Checklist> CreateNewChecklistAsync (Checklist newChecklist)
    {
        return newChecklist;
    }
    public async Task<List<Checklist>> GetChecklistsAsync (Guid userId)
    {
        return await _checklistStorage.GetListsAsync(userId);
    }
    public async Task<Checklist> WriteChecklistAsync (Checklist newList)
    {
        await _checklistStorage.WriteChecklistAsync(newList);
        return newList;
    }
    public async Task<Checklist> WriteUpdatedListAsync(Checklist updatedChecklist)
    {
        return await _checklistStorage.WriteUpdatedListAsync(updatedChecklist);
    }
    public async Task<Checklist?> ReadChecklistFromGuidAsync(Guid checklistId)
    {
        return await _checklistStorage.ReadChecklistFromGuidAsync(checklistId);
    }
    public async Task<bool> DeleteChecklistAsync (Checklist deleteChecklist)
    {
        return await _checklistStorage.DeleteChecklistAsync(deleteChecklist);
    }
}