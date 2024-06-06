namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;

public class UserChange
{
    public Guid userId {get; set;}
    public string? email {get; set;}
    public string? userName {get; set;}
    public string? rawPassword {get; set;}
    public UserChange() {}
}