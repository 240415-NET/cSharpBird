namespace cSharpBird.API;

public interface IBirdService
{
    public Task<List<Bird>> RetrieveLoggedBirdsAsync (Guid checklistId);
    public Task<bool> ValidListUpdate (string userInput);
    public Task<Bird> AddBirdToChecklistAsync (Bird newBird);
    public Task<Bird> UpdateBirdOnChecklistAsync (BirdUpdate info);
}