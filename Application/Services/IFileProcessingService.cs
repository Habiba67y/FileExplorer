using Application.Filters;
using Application.Models;

namespace Application.Services;

public interface IFileProcessingService
{
    ValueTask<StorageFileFilterDataModel> GetFilterDataModelAsync(string directoryPath);
    ValueTask<List<StorageFile>> GetByFilterAsync(StorageFileFilterModel filterModel);
}
