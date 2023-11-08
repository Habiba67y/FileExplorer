using Application.Enums;

namespace Application.Filters;

public class StorageFilesDetails
{
    public StorageFileType FileType { get; set; }
    public string DisplayeName { get; set; }
    public long Count { get; set; }
    public long Size { get; set; }
    public string ImageUrl { get; set; }
}
