using System;
using System.IO;

namespace Orchard.StorageProviders
{
    public interface IFile
    {
        string AbsoluteUrl { get; }
        string Path { get; }
        DateTime LastModified { get; }
        long Length { get; }
        bool IsDirectory { get; }

        /// <summary>Returns the file contents as readonly stream. Caller should dispose stream when complete.</summary>
        /// <returns>The file stream</returns>
        Stream CreateReadStream();
    }
}
