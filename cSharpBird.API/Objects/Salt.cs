namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
public class Salt
{
    [Key]
    public Guid userId {get; set;}
    public string salt {get; set;}
    public Salt () {}
    public Salt (Guid _userId, string _salt)
    {
        userId = _userId;
        salt = _salt;
    }
}