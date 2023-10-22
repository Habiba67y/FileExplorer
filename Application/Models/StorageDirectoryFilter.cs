namespace Application.Models;

public class StorageDirectoryFilter
{
    public int PageToken { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IncludeDirectories { get; set; } = true;
    public bool IncludeFiles { get; set; } = true;
}
