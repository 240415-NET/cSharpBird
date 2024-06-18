namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [HttpGet("Users/{usernameToFind}")]
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
    [HttpPatch("Users/UpdateUser")]
    public async Task<ActionResult<User>> UpdateUser (UserChange UserChanges)
    {
        try
        {
            Console.WriteLine("Running through User Updates!");
            Console.WriteLine("Guid: "+UserChanges.userId);
            Console.WriteLine("Email: "+UserChanges.email);
            Console.WriteLine("Username: "+UserChanges.userName);
            Console.WriteLine("RawPassword: "+UserChanges.rawPassword);
            User updatedUser = _userService.GetUserByGuidAsync(UserChanges.userId).Result;
            if (updatedUser != null)
            {
                if ((UserChanges.email != null || UserChanges.email != "") && _userService.ValidEmail(UserChanges.email))
                {
                    Console.WriteLine("User needs email changed");
                    await _userService.changeEmail(updatedUser,UserChanges.email);
                }
                    
                if (!String.IsNullOrEmpty(UserChanges.userName))
                {
                    Console.WriteLine("User needs to change username");
                    await _userService.changeName(updatedUser,UserChanges.userName);
                }
                    
                if (!String.IsNullOrEmpty(UserChanges.rawPassword))
                {
                    Console.WriteLine("User needs a password change");
                    await _userService.UpdatePassword(UserChanges.rawPassword,updatedUser);
                }
                    
            }
            return Ok(updatedUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}