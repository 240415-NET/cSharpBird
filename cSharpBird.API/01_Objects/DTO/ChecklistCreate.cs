namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;

public class ChecklistCreate
{
    public Guid userId {get; set;}
    public string locationName {get; set;}
    public DateTime? checklistDateTime {get; set;}    
    public ChecklistCreate () {}
}