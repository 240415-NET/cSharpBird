namespace cSharpBird.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class WebBirdController : ControllerBase
{
    private readonly IBirdService _birdService;
    public WebBirdController (IBirdService birdServiceFromBuilder)
    {
        _birdService = birdServiceFromBuilder;
    }
    [HttpPatch("Birds/ListChange")]
    public async Task<ActionResult<Bird>> UpdateBirdAsync (BirdUpdate info)
    {
        return null;
    }
}