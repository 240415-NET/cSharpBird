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
    [HttpPost("Users/Create")]
    //Guess we're changing this to a DTO...
    public async Task<ActionResult<User>> PostNewUser (UserCreate possibleUser)
    {
        Guid tempGuid = Guid.NewGuid();
        var hashedPW = await _userService.InitHashPassword(tempGuid, possibleUser.rawPassword);
        try
        {
            User newUser = new User(tempGuid, possibleUser.userEmail, possibleUser.displayName, hashedPW);
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