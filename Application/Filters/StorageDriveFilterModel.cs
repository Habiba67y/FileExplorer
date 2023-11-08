namespace Application.Filters;

public class StorageDirectoryFilter : FilterPagination
{
    public bool IncludeDirectories { get; set; } = true;
    public bool IncludeFiles { get; set; } = true;
}
