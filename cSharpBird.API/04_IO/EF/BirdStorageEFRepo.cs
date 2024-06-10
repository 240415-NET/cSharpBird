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
    public List<Bird> GetFullBirdList()
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
    }
    public async Task<Checklist> WriteBirdsForChecklist(Checklist checklist)
    {
        Console.WriteLine("Successful call to bird storage");
        //List<Bird> saveBirds = checklist.birds;
        /*for (int i = 0; i < saveBirds.Count; i++)
        {
            _context.Birds.Add(saveBirds[i]);
            Console.WriteLine("Entered for loop");
       }*/
       //Console.WriteLine(checklist.birds[0].bandCode);
       foreach (Bird bird in checklist.birds)
       {
            //_context.Birds.Add(bird);
            Console.WriteLine(bird.bandCode);
       }
        await _context.SaveChangesAsync();
        Console.WriteLine("Successful exit of forloop");
        return checklist;
    }
    public async Task<Checklist> UpdateBirdsForChecklist(Checklist checklist)
    {
        foreach (var bird in checklist.birds)
        {
            if (bird.numSeen > 0 || bird.bbc != null || bird.bNotes != null)
                _context.Birds.Add(bird);
        }
        await _context.SaveChangesAsync();
        return checklist;
    }
    public Task<List<Bird>?> ReadBirdsForChecklist(Guid checklistID)
    {
        return Task.FromResult(GetFullBirdList());
    }
    public void DeleteBirdsForChecklist(Checklist checklist)
    {

    }
}