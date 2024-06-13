namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class WebBirdController : ControllerBase
{
    private readonly IBirdService _birdService;
    public WebBirdController (IBirdService birdServiceFromBuilder)
    {
        _birdService = birdServiceFromBuilder;
    }
    [HttpPost("Birds/AddBird")]
    public async Task<ActionResult<Bird>> AddBirdAsync (BirdUpdate info)
    {
        try{
            Console.WriteLine(info.checklistId);
            Console.WriteLine(info.speciesName);
            Console.WriteLine(info.numSeen);
            Bird newBird = new Bird(info.speciesName,(int)info.numSeen,info.checklistId);
            await _birdService.AddBirdToChecklistAsync(newBird);
            Console.WriteLine(newBird.randomBirdId);
            Console.WriteLine(newBird.checklistId);
            Console.WriteLine(newBird.speciesName);
            Console.WriteLine(newBird.numSeen);
            return Ok(newBird); 
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPatch("Birds/ListChange")]
    public async Task<ActionResult<Bird>> UpdateBirdAsync (BirdUpdate info)
    {

        try{
            Bird updateBird = await _birdService.UpdateBirdOnChecklistAsync(info);
            return Ok(updateBird);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}