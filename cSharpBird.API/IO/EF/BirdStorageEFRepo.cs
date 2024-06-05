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
    public async void WriteBirdsForChecklist(Checklist checklist)
    {

    }
    public void UpdateBirdsForChecklist(Checklist checklist)
    {

    }
    public Task<List<Bird>?> ReadBirdsForChecklist(Guid checklistID)
    {
        return Task.FromResult(GetFullBirdList());
    }
    public void DeleteBirdsForChecklist(Checklist checklist)
    {

    }
}