namespace cSharpBird.API;

using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

public class BirdStorageEFRepo : IBirdStorageEF
{
    private readonly cSharpBirdContext _context;
    string _connectionstring = File.ReadAllText("C:\\Users\\U0LA19\\Documents\\cSharpBirdWeb_DataSource.txt");
    public BirdStorageEFRepo(cSharpBirdContext contextFromBuilder)
    {
        _context = contextFromBuilder;
    }
    /*public List<Bird> GetFullBirdList()
    {
        string bandCode = "";
        string speciesName = "";
        string path = "data\\BirdCSV\\";
        string pathFile = path + "USGSBBL.csv";
        List<Bird> birdList = new List<Bird>();
        birdList = File.ReadAllLines(pathFile)
            .Select(line => line.Split(','))
            .Select(x => new Bird{
                bandCode = x[0],
                speciesName = x[1]
            }).ToList();

        return birdList;
    }*/
    public async Task<Checklist> WriteBirdsForChecklist(Checklist checklist)
    {
        foreach (Bird bird in checklist.birds)
        {
            _context.Birds.Add(bird);
        }
        await _context.SaveChangesAsync();
        return checklist;
    }
    public async Task<Checklist> UpdateBirdsForChecklist(Checklist checklist)
    {
        foreach (var bird in checklist.birds)
        {
            _context.Birds.Update(bird);
        }
        await _context.SaveChangesAsync();
        return checklist;
    }
    public Task<List<Bird>?> ReadBirdsForChecklist(Guid checklistID)
    {
        List<Bird> checklistBirds = new List<Bird>();
        var cBirds = from c in _context.Birds select c;
        cBirds = cBirds.Where(c => c.checklistId.Equals(checklistID));
        checklistBirds = cBirds.ToList();
        return Task.FromResult(checklistBirds);
    }
    public async Task<Checklist> DeleteBirdsForChecklist(Checklist checklist)
    {
        foreach (Bird bird in checklist.birds)
        {
            _context.Remove(bird);
        }
        await _context.SaveChangesAsync();
        return checklist;
    }
    public async Task<Bird> IndividualWriteBirdForChecklist (Bird bird)
    {
        _context.Birds.Add(bird);
        await _context.SaveChangesAsync();
        return bird;
    }
    public async Task<Bird> IndividualUpdateBirdForChecklist (BirdUpdate info)
    {
        Bird bird = await _context.Birds.FirstOrDefaultAsync(c => c.checklistId == info.checklistId && c.speciesName == info.speciesName); 
        Console.WriteLine(bird.speciesName);
        Console.WriteLine(bird.randomBirdId);
        _context.Update(bird);
        await _context.SaveChangesAsync();
        return bird;
    }
}