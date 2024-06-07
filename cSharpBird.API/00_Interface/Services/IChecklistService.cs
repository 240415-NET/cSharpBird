namespace cSharpBird.API;

public interface IChecklistService
{
    public Task<Checklist> CreateNewChecklistAsync (Checklist newChecklist);
    public Task<List<Checklist>> GetChecklistsAsync (User user);
    public Task<Checklist> WriteChecklistAsync (Checklist newList);
    public Task<Checklist> WriteUpdatedListAsync(Checklist updatedChecklist);
    public Task<bool> DeleteChecklistAsync (Checklist deleteChecklist);
}