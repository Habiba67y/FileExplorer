using Application.Models;

namespace Application.Services;

public interface IEntryService
{
    ValueTask<List<IStorageEntry>> Get(string directoryPath, FilterPagination filter);
}
