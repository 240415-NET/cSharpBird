namespace cSharpBird.API;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

public class ChecklistStorageEFRepo : IChecklistStorageEF
{
    private readonly cSharpBirdContext _context;
    //string _connectionstring = ConnectionStringHelper.GetConnectionString();
    public ChecklistStorageEFRepo(cSharpBirdContext contextFromBuilder)
    {
        _context = contextFromBuilder;
    }
    public async Task<List<Checklist?>?> GetListsAsync(Guid searchUser)
    {
        List<Checklist> userChecklists = new List<Checklist>();
        var uChecklists = from c in _context.Checklists select c;
        uChecklists = uChecklists.Where(c => c.userId.Equals(searchUser));
        userChecklists = uChecklists.ToList();
        return userChecklists;
    }    
    public async Task<Checklist> WriteChecklistAsync(Checklist newList)
    {
        _context.Checklists.Add(newList);
        await _context.SaveChangesAsync();
        return newList;
    }

    public async Task<Checklist> WriteUpdatedListAsync(Checklist updatedList)
    {
        Checklist? existingChecklist = await _context.Checklists.FirstOrDefaultAsync(c => c.checklistID == updatedList.checklistID);
        if (existingChecklist != null)
        {
            existingChecklist.checklistDateTime = updatedList.checklistDateTime;
            existingChecklist.locationName = updatedList.locationName;
            existingChecklist.birds = updatedList.birds;
        }
        await _context.SaveChangesAsync();
        return existingChecklist;
    }
    public async Task<Checklist> ReadChecklistFromGuidAsync (Guid checklistId)
    {
        Checklist? existingChecklist = await _context.Checklists.FirstOrDefaultAsync(c => c.checklistID == checklistId);
        return existingChecklist;
    }
    public async Task<bool> DeleteChecklistAsync(Guid checklistIdToDelete)

    {
        //Gonna wait for Jonathan to solve this one
        Checklist checklistToBeDeleted = await ReadChecklistFromGuidAsync(checklistIdToDelete);
        if (checklistToBeDeleted ==null)
        {
            throw new Exception("thrown from DB layer, checklistToBeDeleted was null");
        }
        _context.Checklists.Remove(checklistToBeDeleted);

        await _context.SaveChangesAsync();

        return true;
    }
}