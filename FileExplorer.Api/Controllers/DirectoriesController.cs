using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileExplorer.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DirectoriesController : ControllerBase
{
    private readonly IEntryService _entryService;
    public DirectoriesController(IEntryService entryService)
    {
        _entryService = entryService;
    }

    [HttpGet("root/entries")]
    public async ValueTask<IActionResult> GetRootEntries([FromQuery] FilterPagination filter, [FromServices] IWebHostEnvironment environment)
    {
        var result = await _entryService.Get(environment.WebRootPath, filter);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{directoryPath}/entries")]
    public async ValueTask<IActionResult> GetRootEntries([FromRoute] string directoryPath, [FromQuery] FilterPagination filter)
    {
        var result = await _entryService.Get(directoryPath, filter);
        return result.Any() ? Ok(result) : NoContent();
    }
}
