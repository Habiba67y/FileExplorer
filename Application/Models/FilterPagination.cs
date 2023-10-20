namespace Application.Models;

public class FilterPagination
{
    public int PageToken { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IncludeDirectories { get; set; } = true;
    public bool IncludeFiles { get; set; } = true;
}
