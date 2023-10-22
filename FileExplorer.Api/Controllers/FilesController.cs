using Application.Models;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorer.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class FilesController : ControllerBase
{
    private readonly IEntryService _entryService;
    public FilesController(IEntryService entryService)
    {
        _entryService = entryService;
    }

    [HttpGet("root/files")]
    public async ValueTask<IActionResult> GetRootEntries([FromQuery] StorageFileFilter filter, [FromServices] IWebHostEnvironment environment)
    {
        var result = await _entryService.GetFiles(environment.WebRootPath, filter);
        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{directoryPath}/files")]
    public async ValueTask<IActionResult> GetRootEntries([FromRoute] string directoryPath, [FromQuery] StorageFileFilter filter)
    {
        var result = await _entryService.GetFiles(directoryPath, filter);
        return result.Any() ? Ok(result) : NoContent();
    }
}
