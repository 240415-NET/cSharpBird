namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;

public class BirdUpdate
{
    public Guid checklistId {get; set;}
    public string? speciesName {get; set;}  
    public int? numSeen {get; set;}
    public BirdUpdate () {}
}