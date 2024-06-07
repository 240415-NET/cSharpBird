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
    private readonly IUserService _userService;
    public WebChecklistController (IUserService userServiceFromBuilder)
    {
        _userService = userServiceFromBuilder;
    }
    [HttpPost("Checklists/Create")]
    public async Task<ActionResult<User>> PostNewChecklist (UserCreate possibleUser)
    {
        Guid tempGuid = Guid.NewGuid();
        var hashedPW = await _userService.InitHashPassword(tempGuid, possibleUser.rawPassword);
        try
        {
            User newChecklist = new Checklist;
            await _userService.CreateNewUserAsync(newUser);
            return Ok(newUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}