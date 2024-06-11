namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
public class Checklist
{
    [Key]
    public Guid checklistID {get; set;}
    public Guid userId {get; set;}
    public string locationName {get; set;}
    public DateTime checklistDateTime {get; set;}
    public List<Bird>? birds {get; set;} = new();//currently every entry in the CSV files
    public Checklist() {}

    public Checklist(Guid _userId, string _locationName)
    {
        checklistID = Guid.NewGuid();
        userId = _userId;
        locationName = _locationName;
        checklistDateTime = DateTime.Today;
    }
    public Checklist(Guid _userId, string _locationName, string _checklistDateTime)
    {
        checklistID = Guid.NewGuid();
        userId = _userId;
        locationName = _locationName;
        checklistDateTime = DateTime.Parse(_checklistDateTime);
    }
    /*
    public Checklist(Guid _checklistID, Guid _userId, string _locationName, DateTime _checklistDateTime, List<Bird> _birds, float _distance, int _duration, bool _stationary, string _cNotes)
    {
        checklistID = _checklistID;
        userId = _userId;
        locationName = _locationName;
        checklistDateTime = _checklistDateTime;
        List<Bird> birds = GetFullBirdList();
        birds = _birds;
        distance = _distance;
        duration = _duration;
        stationary = _stationary;
        cNotes = _cNotes;
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
    }*/
}