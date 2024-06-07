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
    [HttpPost("Users/SignIn")]
    public async Task<ActionResult<User>> SignInUser (SignIn userSignIn)
    {
        try
        {
            User signInAttempt = _userService.GetUserByUsernameAsync(userSignIn.userName).Result;
            if (signInAttempt == null)
                return NotFound("Incorrect username or password");
            else if((bool)_userService.VerifyPassword(userSignIn.rawPassword,signInAttempt).Result)
                return Ok(signInAttempt);
            else
                return NotFound("Incorrect username or password");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpPost("Users/UpdateUser")]
    public async Task<ActionResult<User>> UpdateUser (UserChange UserChanges)
    {
        try
        {
            User updatedUser = _userService.GetUserByGuidAsync(UserChanges.userId).Result;
            if (updatedUser != null)
            {
                if (UserChanges.email != null || UserChanges.email != "")
                    await _userService.changeEmail(updatedUser,UserChanges.email);
                if (UserChanges.userName != null || UserChanges.userName != "")
                    await _userService.changeName(updatedUser,UserChanges.userName);
                if (UserChanges.rawPassword != null || UserChanges.rawPassword != "")
                    await _userService.UpdatePassword(UserChanges.rawPassword,updatedUser);
            }
            return Ok(updatedUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}