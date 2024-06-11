namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Bird
{
    [Key]
    public Guid randomBirdId {get; set;}
    public string speciesName {get; set;}
    public int numSeen {get; set;}
    public Bird() {}
    public Bird(string _speciesName, int _numSeen)
    {
        randomBirdId = new Guid();
        speciesName = _speciesName;
        numSeen = _numSeen;
    }
}