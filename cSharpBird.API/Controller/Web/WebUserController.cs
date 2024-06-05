namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class WebUserController : ControllerBase
{
    private readonly IUserService _userService;
    public WebUserController (IUserService userServiceFromBuilder)
    {
        _userService = userServiceFromBuilder;
    }
    [HttpPost("Users/{username}")]
    //Guess we're changing this to a DTO...
    public async Task<ActionResult<User>> PostNewUser (string userName, string password)
    {
        try
        {
            User newUser = new User (userName,password);
            await _userService.CreateNewUserAsync(newUser);
            return Ok(newUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("Users/{usernameToFind}")]
    public async Task<ActionResult<User>> GetUserByUserName (string usernameToFind)
    {
        try
        {
            return await _userService.GetUserByUsernameAsync (usernameToFind);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}