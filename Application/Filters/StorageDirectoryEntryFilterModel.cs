namespace Application.Filters;

public class StorageDirectoryEntryFilterModel : FilterPagination
{
    public bool IncludeDirectories { get; set; } = true;
    public bool IncludeFiles { get; set; } = true;
}
