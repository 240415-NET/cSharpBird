namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;

public class UserCreate
{
    public string userEmail {get; set;}
    public string displayName {get; set;}
    public string rawPassword {get; set;}
    public UserCreate() {}
}