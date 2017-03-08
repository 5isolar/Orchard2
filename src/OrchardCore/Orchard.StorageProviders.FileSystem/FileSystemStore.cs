using System;
using System.Collections.Generic;
using System.IO;

namespace Orchard.StorageProviders.FileSystem
{
    public class FileSystemStore : IFileStore
    {
        private readonly string _localPathPrefix;
        private readonly string _publicPathPrefix;

        public string LocalBasePath => _localPathPrefix;

        public FileSystemStore(string localPathPrefix, string publicPathPrefix)
        {
            _localPathPrefix = localPathPrefix;
            _publicPathPrefix = publicPathPrefix;
        }

        public string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        public bool TryCopyFile(string originalPath, string duplicatePath)
        {
            try
            {
                File.Copy(GetPhysicalPath(originalPath), GetPhysicalPath(duplicatePath));
                return true;
            }
            catch
            {
                return false;
            }            
        }

        public bool TryDeleteFile(string subpath)
        {
            try
            {
                File.Delete(GetPhysicalPath(subpath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryDeleteFolder(string subpath)
        {
            try
            {
                Directory.Delete(GetPhysicalPath(subpath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<IFile> GetDirectoryContent(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFile GetFile(string subpath)
        {
            var fileInfo = new FileInfo(GetPhysicalPath(subpath));

            if (fileInfo.Exists)
            {
                return new FileSystemFile(subpath, _publicPathPrefix, fileInfo);
            }

            return null;
        }

        public bool TryCreateFolder(string subpath)
        {
            try
            {
                Directory.CreateDirectory(GetPhysicalPath(subpath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IFile MapFile(string absoluteUrl)
        {
            if (!absoluteUrl.StartsWith(_publicPathPrefix, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return GetFile(GetPhysicalPath(absoluteUrl.Substring(_publicPathPrefix.Length)));
        }

        public bool TryMoveFile(string oldPath, string newPath)
        {
            try
            {
                File.Move(GetPhysicalPath(oldPath), GetPhysicalPath(newPath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TryMoveFolder(string oldPath, string newPath)
        {
            try
            {
                Directory.Move(GetPhysicalPath(oldPath), GetPhysicalPath(newPath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool TrySaveStream(string subpath, Stream inputStream)
        {
            throw new NotImplementedException();
        }

        private string GetPhysicalPath(string subpath)
        {
            string physicalPath = string.IsNullOrEmpty(subpath) ? _localPathPrefix : Path.Combine(_localPathPrefix, subpath);
            return ValidatePath(_localPathPrefix, physicalPath);
        }

        /// <summary>
        /// Determines if a path lies within the base path boundaries.
        /// If not, an exception is thrown.
        /// </summary>
        /// <param name="basePath">The base path which boundaries are not to be transposed.</param>
        /// <param name="physicalPath">The path to determine.</param>
        /// <rereturns>The mapped path if valid.</rereturns>
        /// <exception cref="ArgumentException">If the path is invalid.</exception>
        public static string ValidatePath(string basePath, string physicalPath)
        {
            bool valid = false;

            try
            {
                // Check that we are indeed within the storage directory boundaries
                valid = Path.GetFullPath(physicalPath).StartsWith(Path.GetFullPath(basePath), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                // Make sure that if invalid for medium trust we give a proper exception
                valid = false;
            }

            if (!valid)
            {
                throw new ArgumentException("Invalid path");
            }

            return physicalPath;
        }
    }
}
