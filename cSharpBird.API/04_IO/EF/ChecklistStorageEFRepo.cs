namespace cSharpBird.API;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

public class ChecklistStorageEFRepo : IChecklistStorageEF
{
    private readonly cSharpBirdContext _context;
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBirdWeb_DataSource.txt");
    public ChecklistStorageEFRepo(cSharpBirdContext contextFromBuilder)
    {
        _context = contextFromBuilder;
    }
    public async Task<List<Checklist?>?> GetListsAsync(User searchUser)
    {
        List<Checklist> userChecklists = new List<Checklist>();
        var uChecklists = from c in _context.Checklists select c;
        uChecklists = uChecklists.Where(c => c.userId.Equals(searchUser.userId));
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
            existingChecklist.distance = updatedList.distance;
            existingChecklist.duration = updatedList.duration;
            existingChecklist.stationary = updatedList.stationary;
            existingChecklist.cNotes = updatedList.cNotes;
        }
        await _context.SaveChangesAsync();
        return existingChecklist;
    }
    public async Task<bool> DeleteChecklistAsync(Checklist deleteChecklist)
    {
        //Gonna wait for Jonathan to solve this one
        Checklist toBeDeleted = deleteChecklist;
        return true;
    }
}