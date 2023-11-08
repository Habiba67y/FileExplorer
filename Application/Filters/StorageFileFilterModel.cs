using Application.Enums;

namespace Application.Filters;

public class StorageFileFilterModel : FilterPagination
{
    public string DirectoryPath { get; set; }
    public List<StorageFileType> FilesType { get; set; }
}
