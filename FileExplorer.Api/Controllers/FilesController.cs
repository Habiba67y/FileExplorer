using Application.Filters;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorer.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class FilesController : ControllerBase
{
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IFileProcessingService _fileProcessingService;
    public FilesController(IWebHostEnvironment hostEnvironment, IFileProcessingService entryService)
    {
        _hostEnvironment = hostEnvironment;
        _fileProcessingService = entryService;
    }

    [HttpGet("root/files/filter")]
    public async ValueTask<IActionResult> GetFilterData()
    {
        var result = await _fileProcessingService.GetFilterDataModelAsync(_hostEnvironment.WebRootPath);

        return Ok(result);
    }

    [HttpGet("root/files/by-filter")]
    public async ValueTask<IActionResult> GetRootEntries([FromQuery] StorageFileFilterModel filterModel)
    {
        filterModel.DirectoryPath = _hostEnvironment.WebRootPath;
        var result = await _fileProcessingService.GetByFilterAsync(filterModel);

        return result.Any() ? Ok(result) : NotFound(result);
    }
}
