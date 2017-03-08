using System;
using System.IO;

namespace Orchard.StorageProviders.FileSystem
{
    public class FileSystemFile : IFile
    {
        private readonly FileInfo _fileInfo;
        private readonly string _path;
        private readonly string _publicPathPrefix;

        public FileSystemFile(string path, string publicPathPrefix, FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
            _path = path;
            _publicPathPrefix = publicPathPrefix;
        }

        public string AbsoluteUrl => _publicPathPrefix + _path;

        public string Path => _path;

        public long Length => _fileInfo != null ? _fileInfo.Length : 0;

        public DateTime LastModified => _fileInfo != null ? _fileInfo.LastWriteTimeUtc : DateTime.MinValue;

        public bool IsDirectory => _fileInfo != null;

        public Stream CreateReadStream() => _fileInfo != null ? _fileInfo.OpenRead() : null;
    }
}
