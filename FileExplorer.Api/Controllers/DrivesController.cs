using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FileExplorer.Api.Controllers;
[ApiController]
[Route("api/[controller]")]

public class DrivesController : ControllerBase
{
    private readonly IDriveService _driveService;
    public DrivesController(IDriveService driveService)
    {
        _driveService = driveService;
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetDrives()
    {
        var result = await _driveService.GetDrives();
        return result.Any() ? Ok(result) : NoContent();
    }
}
