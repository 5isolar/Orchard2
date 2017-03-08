using System;
using System.Collections.Generic;
using System.IO;

namespace Orchard.StorageProviders
{
    public interface IFileStore
    {
        /// <summary>
        /// Checks if the given file exists within the file store.
        /// </summary>
        /// <param name="subpath">The relative path within the file system.</param>
        /// <returns>A <see cref="IFile"/> object representing the result of .</returns>
        IFile GetFile(string subpath);

        /// <summary>
        /// Retrieves a within the file store for a public url.
        /// </summary>
        /// <param name="absoluteUrl">The public url of a media.</param>
        /// <returns>An <see cref="IFile"/> object retpresenting the file found.</returns>
        IFile MapFile(string absoluteUrl);

        /// <summary>
        /// Enumerates the files at the given subpath.
        /// </summary>
        /// <param name="subpath">The relative path of the folder to enumerate.</param>
        /// <returns>The list of files in the folder.</returns>
        IEnumerable<IFile> GetDirectoryContent(string subpath);

        /// <summary>
        /// Creates a folder in the file store if it doesn't exist.
        /// </summary>
        /// <param name="subpath">The path to the folder to be created.</param>
        bool TryCreateFolder(string subpath);

        /// <summary>
        /// Deletes a folder in the file store if it exists.
        /// </summary>
        /// <param name="subpath">The path to the folder to be deleted.</param>
        bool TryDeleteFolder(string subpath);

        /// <summary>
        /// Moves a folder in the file store.
        /// </summary>
        /// <param name="oldPath">The path to the folder to be renamed.</param>
        /// <param name="newPath">The path to the new folder.</param>
        bool TryMoveFolder(string oldPath, string newPath);

        /// <summary>
        /// Deletes a file in the file store if it exists.
        /// </summary>
        /// <param name="subpath">The path to the file to be deleted.</param>
        bool TryDeleteFile(string subpath);

        /// <summary>
        /// Moves a file in the file store.
        /// </summary>
        /// <param name="oldPath">The path to the file to be renamed.</param>
        /// <param name="newPath">The path to the new file.</param>
        bool TryMoveFile(string oldPath, string newPath);

        /// <summary>
        /// Copies a file in the file store.
        /// </summary>
        /// <param name="originalPath">The relative path to the file to be copied.</param>
        /// <param name="duplicatePath">The relative path to the new file.</param>
        bool TryCopyFile(string originalPath, string duplicatePath);

        /// <summary>
        /// Tries to save a stream in the file store.
        /// </summary>
        /// <param name="subpath">The relative path to the file to be created.</param>
        /// <param name="inputStream">The stream to be saved.</param>
        /// <returns><c>True</c> if success; <c>False</c> otherwise.</returns>
        bool TrySaveStream(string subpath, Stream inputStream);

        /// <summary>
        /// Combines multiple path parts.
        /// </summary>
        /// <param name="paths">The paths to combine.</param>
        /// <returns>The combined path.</returns>
        string Combine(params string[] paths);
    }
}
