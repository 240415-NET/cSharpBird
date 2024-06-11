using Microsoft.Identity.Client;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace cSharpBird.API;

public class BirdService : IBirdService
{
    private readonly IBirdStorageEF _birdStorage;
    public BirdService (IBirdStorageEF efRepoFromBuilder)
    {
        _birdStorage = efRepoFromBuilder;
    }
    public async Task<List<Bird>> RetrieveLoggedBirdsAsync (Guid checklistId)
    {
        return await _birdStorage.ReadBirdsForChecklist(checklistId);
    }
    public async Task<bool> ValidListUpdate (string userInput)
    {
        //this method validates the data that was given by the user to add species to the checklist
        userInput = userInput.Trim();
        string[] input = userInput.Split(' ',',');
        bool valid = false;
        //confirms that there are two substrings, one for a given species and one for a valid integer
        if (input[0].Length == 4 && Convert.ToInt64(input[1]) > -1)
            valid = true;
        if  (userInput.Length <= 4)
            valid = false;
        return valid;
    }
    public async Task<Bird> AddBirdToChecklistAsync (Bird newBird)
    {
        return await _birdStorage.IndividualWriteBirdForChecklist(newBird);
    }
    public async Task<Bird> UpdateBirdOnChecklistAsync (BirdUpdate info)
    {
        return await _birdStorage.IndividualUpdateBirdForChecklist(info);
    }
}