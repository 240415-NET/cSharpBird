namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class Bird
{
    public string bandCode {get; set;}
    public string speciesName {get; set;}

    public int numSeen {get; set;}
    public string? bbc {get;set;}
    public string? bNotes {get;set;}
    public Bird() {}
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