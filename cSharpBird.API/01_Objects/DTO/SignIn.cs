namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel.DataAnnotations;

public class SignIn
{
    public string userName {get; set;}
    public string rawPassword {get; set;}
    public SignIn() {}
}