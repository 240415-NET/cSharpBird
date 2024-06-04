namespace cSharpBird;
using System;
using System.Collections.Generic;
using System.IO;
public class Location
{
    public Guid locationID {get; set;}
    public string locationName {get; set;}
    public string county {get; set;}
    public string state {get; set;}
    public Location() {}

    public Location(string _locationName)
    {
        locationID = Guid.NewGuid(); 
        locationName = _locationName;
        county = "Pinellas";
        state = "Florida";
    }
     public Location(string _locationName, string _state, string _county)
    {
        locationID = Guid.NewGuid(); 
        locationName = _locationName;
        county = _county;
        state = _state;
    }
}