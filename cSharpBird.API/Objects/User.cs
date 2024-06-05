namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
public class User
{
    [Key]
    public Guid userId {get; set;}
    public string userName {get; set;}
    public string displayName {get; set;}
    public string hashedPW {get; set;}
    private readonly IUserService _userService;
    public User (IUserService userServiceFromBuilder)
    {
        _userService = userServiceFromBuilder;
    }
    public User() {}

    public User(string _userName,string password)
    {
        userId = Guid.NewGuid(); 
        userName = _userName;
        displayName = "";
        hashedPW = _userService.InitHashPassword(userId,password);
    }
    public User(Guid _userId, string _userName, string _displayName, string _hashedPW)
    {
        userId = _userId;
        userName = _userName;
        displayName = _displayName;
        hashedPW = _hashedPW;
    }
}