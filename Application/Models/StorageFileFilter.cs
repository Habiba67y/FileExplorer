namespace Application.Models;

public class StorageFileFilter
{
    public int PageToken { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IncludeDocumentFiles { get; set; } = true;
    public bool IncludeImageFiles { get; set; } = true;
    public bool IncludeSourceCodeFiles { get; set; } = true;
}
