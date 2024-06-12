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
        List<Checklist> userLists = await _checklistStorage.GetListsAsync(userId);
        foreach (Checklist check in userLists)
        {
            check.birds = await _birdStorage.ReadBirdsForChecklist(check.checklistID);
        }
        return userLists;
    }
    public async Task<Checklist> WriteChecklistAsync (Checklist newList)
    {
        await _checklistStorage.WriteChecklistAsync(newList);
        return newList;
    }
    public async Task<Checklist> WriteUpdatedListAsync(Checklist updatedChecklist)
    {
        await _checklistStorage.WriteUpdatedListAsync(updatedChecklist);
        await _birdStorage.UpdateBirdsForChecklist(updatedChecklist);
        return updatedChecklist;
    }
    public async Task<Checklist?> ReadChecklistFromGuidAsync(Guid checklistId)
    {
        Checklist foundChecklist = await _checklistStorage.ReadChecklistFromGuidAsync(checklistId);
        foundChecklist.birds = await _birdStorage.ReadBirdsForChecklist(checklistId);
        return foundChecklist;        
    }
    public async Task<bool> DeleteChecklistAsync (Checklist deleteChecklist)
    {
        await _checklistStorage.DeleteChecklistAsync(deleteChecklist);
        await _birdStorage.DeleteBirdsForChecklist(deleteChecklist);
        return true;
    }
}