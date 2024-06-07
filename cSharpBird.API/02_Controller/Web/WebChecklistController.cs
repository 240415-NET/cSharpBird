namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class WebChecklistController : ControllerBase
{
    private readonly IChecklistService _checklistService;
    public WebChecklistController (IChecklistService checklistServiceFromBuilder)
    {
        _checklistService = checklistServiceFromBuilder;
    }
    [HttpPost("Checklists/Create")]
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
}