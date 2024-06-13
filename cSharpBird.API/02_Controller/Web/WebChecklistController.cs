namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class WebChecklistController : ControllerBase
{
    private readonly IChecklistService _checklistService;
    public WebChecklistController (IChecklistService checklistServiceFromBuilder)
    {
        _checklistService = checklistServiceFromBuilder;
    }
    [HttpPost("Checklists/CreateToday")]
    public async Task<ActionResult<Checklist>> PostNewChecklistToday(ChecklistCreate checklistBits)
    {
        try
        {
            Checklist newChecklist = new Checklist (checklistBits.userId, checklistBits.locationName);
            await _checklistService.WriteChecklistAsync(newChecklist);
            return Ok(newChecklist);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("Checklists/Create")]
    public async Task<ActionResult<Checklist>> PostNewChecklistHistoric(ChecklistCreate checklistBits)
    {
        try
        {
            Guid tempGuid = Guid.NewGuid();
            Checklist newChecklist = new Checklist (checklistBits.userId, checklistBits.locationName, checklistBits.checklistDateTime,tempGuid);
            await _checklistService.WriteChecklistAsync(newChecklist);
            Console.WriteLine(newChecklist.checklistID);
            Console.WriteLine(newChecklist.locationName);
            Console.WriteLine(newChecklist.checklistDateTime);
            return Ok(newChecklist);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("Checklists/ListChecklist/{userId}")]
    public async Task<ActionResult<List<Checklist>>> ListUserChecklists (Guid userId)
    {
        try
        {
            List<Checklist> userChecklists = new List<Checklist>();
            userChecklists = await _checklistService.GetChecklistsAsync(userId);
            userChecklists = userChecklists.OrderByDescending(x => x.checklistDateTime).ToList();
            return Ok(userChecklists);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("Checklists/ListBirds")]
    public async Task<ActionResult<List<Bird>>> ListChecklistBirds (Guid checklistId)
    {
        try
        {
            Checklist currChecklist = await _checklistService.ReadChecklistFromGuidAsync(checklistId);
            List<Bird> birdsChecklist = currChecklist.birds;
            return Ok(birdsChecklist);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
} 