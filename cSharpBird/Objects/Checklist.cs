namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class Checklist
{
    public Guid checklistID {get; set;}
    public Guid userId {get;set;}
    public string locationName {get; set;}
    public DateTime checklistDateTime {get; set;}
    public List<Bird> birds {get;set;}
    public float distance {get;set;}
    public int duration {get;set;}
    public bool stationary {get;set;}
    public string cNotes {get;set;}
    public Checklist() {}

    public Checklist(Guid _userId, string _locationName)
    {
        checklistID = Guid.NewGuid();
        userId = _userId;
        locationName = _locationName;
        checklistDateTime = DateTime.Today;
        List<Bird> birds = BirdController.GetFullBirdList();
        stationary = false;
        cNotes = "";
    }
    public Checklist(Guid _userId, string _locationName, string _checklistDateTime)
    {
        checklistID = Guid.NewGuid();
        userId = _userId;
        locationName = _locationName;
        checklistDateTime = DateTime.Parse(_checklistDateTime);
        List<Bird> birds = BirdController.GetFullBirdList();
        stationary = false;
        cNotes = "";
    }
    public Checklist(Guid _checklistID, Guid _userId, string _locationName, DateTime _checklistDateTime, List<Bird> _birds, float _distance, int _duration, bool _stationary, string _cNotes)
    {
        checklistID = _checklistID;
        userId = _userId;
        locationName = _locationName;
        checklistDateTime = _checklistDateTime;
        List<Bird> birds = BirdController.GetFullBirdList();
        birds = _birds;
        distance = _distance;
        duration = _duration;
        stationary = _stationary;
        cNotes = _cNotes;
    }
}