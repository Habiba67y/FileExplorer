using Application.Filters;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileExplorer.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DirectoriesController : ControllerBase
{
    private readonly IDirectoryProcessingService _entryService;
    public DirectoriesController(IDirectoryProcessingService entryService)
    {
        _entryService = entryService;
    }

    [HttpGet("root/entries")]
    public async ValueTask<IActionResult> GetRootEntries([FromQuery] StorageDirectoryEntryFilterModel filter, [FromServices] IWebHostEnvironment environment)
    {
        var result = await _entryService.Get(environment.WebRootPath, filter);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{directoryPath}/entries")]
    public async ValueTask<IActionResult> GetRootEntries([FromRoute] string directoryPath, [FromQuery] StorageDirectoryEntryFilterModel filter)
    {
        var result = await _entryService.Get(directoryPath, filter);
        return result.Any() ? Ok(result) : NoContent();
    }
}
