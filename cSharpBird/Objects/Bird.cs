namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class Bird
{
    //public Guid speciesID {get; set;}
    //public long speciesNum {get;set;} //replaces speciesID GUID
    public string bandCode {get; set;}
    public string speciesName {get; set;}
    //public string rarity {get; set;}

    public int numSeen {get; set;}
    public string? bbc {get;set;}
    public string? bNotes {get;set;}
    public Bird() {}
    /*
    public Bird(string _bandCode, int _numSeen)
    {
        speciesID = Guid.NewGuid(); 
        bandCode = _bandCode;
        numSeen = _numSeen;
    }
    public Bird(string _bandCode, int _numSeen, string _speciesName)
    {
        speciesID = Guid.NewGuid(); 
        bandCode = _bandCode;
        speciesName = _speciesName;
        numSeen = _numSeen;
    }
    
    public Bird(string _bandCode, string _speciesName, string _rarity)
    {
        //speciesID = Guid.NewGuid(); 
        bandCode = _bandCode;
        speciesName = _speciesName;
        rarity = _rarity;
    }

    public Bird(long _speciesNum, string _bandCode, string _speciesName)
    {
        speciesNum = _speciesNum;
        bandCode = _bandCode;
        speciesName = _speciesName;
    }
    */
    public Bird(string _bandCode, string _speciesName)
    {
        bandCode = _bandCode;
        speciesName = _speciesName;
    }
    public Bird(string _bandCode, string _speciesName, int _numSeen, string _bbc, string _bNotes)
    {
        bandCode = _bandCode;
        speciesName = _speciesName;
        numSeen = _numSeen;
        bbc = _bbc;
        bNotes = _bNotes;
    }
}