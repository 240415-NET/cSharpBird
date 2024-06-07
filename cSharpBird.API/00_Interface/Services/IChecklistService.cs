namespace cSharpBird.API;

public interface IChecklistService
{
    public Task<Checklist> CreateNewChecklistAsync (Checklist newChecklist);
    public Task<List<Checklist>> GetChecklistsAsync (Guid userId);
    public Task<Checklist> WriteChecklistAsync (Checklist newList);
    public Task<Checklist> WriteUpdatedListAsync(Checklist updatedChecklist);
    public Task<Checklist?> ReadChecklistFromGuidAsync(Guid checklistId);
    public Task<bool> DeleteChecklistAsync (Checklist deleteChecklist);
}