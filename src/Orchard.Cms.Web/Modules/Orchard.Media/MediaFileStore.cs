using System.Collections.Generic;
using System.IO;
using Orchard.StorageProviders;

namespace Orchard.Media
{
    public class MediaFileStore : IMediaFileStore
    {
        private readonly IFileStore _fileStore;

        public MediaFileStore(IFileStore fileStore)
        {
            _fileStore = fileStore;
        }

        public string Combine(params string[] paths)
        {
            return _fileStore.Combine(paths);
        }

        public IEnumerable<IFile> GetDirectoryContent(string subpath)
        {
            return _fileStore.GetDirectoryContent(subpath);
        }

        public IFile GetFile(string subpath)
        {
            return _fileStore.GetFile(subpath);
        }

        public IFile MapFile(string absoluteUrl)
        {
            return _fileStore.MapFile(absoluteUrl);
        }

        public bool TryCopyFile(string originalPath, string duplicatePath)
        {
            return _fileStore.TryCopyFile(originalPath, duplicatePath);
        }

        public bool TryCreateFolder(string subpath)
        {
            return _fileStore.TryCreateFolder(subpath);
        }

        public bool TryDeleteFile(string subpath)
        {
            return _fileStore.TryDeleteFile(subpath);
        }

        public bool TryDeleteFolder(string subpath)
        {
            return _fileStore.TryDeleteFolder(subpath);
        }

        public bool TryMoveFile(string oldPath, string newPath)
        {
            return _fileStore.TryMoveFile(oldPath, newPath);
        }

        public bool TryMoveFolder(string oldPath, string newPath)
        {
            return _fileStore.TryMoveFolder(oldPath, newPath);
        }

        public bool TrySaveStream(string subpath, Stream inputStream)
        {
            return _fileStore.TrySaveStream(subpath, inputStream);
        }
    }
}
