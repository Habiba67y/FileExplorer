﻿using Application.Enums;
using Application.Filters;
using Application.Models;

namespace Application.Services;

public interface IFileService
{
    ValueTask<StorageFile> GetByPath(string path);
    ValueTask<List<StorageFile>> GetFilesByPath(IEnumerable<string> directoriesPath);
    IEnumerable<StorageFilesDetails> GetFilesDetails(IEnumerable<StorageFile> files);
    StorageFileType GetFileType(string filePath);
}
