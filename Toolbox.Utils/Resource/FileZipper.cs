// ===================================================================================================
// = Author      :  Mike
// = Created     :  2020-06-08 19:01
// ===================================================================================================
// = Description :
// ===================================================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Com.Toolbox.Utils.Memory
{
	/// <summary>
	///     Provides operations to zip a collection of
	///     files in memory.
	/// </summary>
	public static class FileZipper
	{
		#region Public Methods

		/// <summary>
		///     Creates a new <see cref="ZipArchive"/> from the
		///     passed <paramref name="files"/> and writes the
		/// 	created <see cref="ZipArchive"/> into a stream. The
		///     returned stream is not closed.
		/// </summary>
		/// <param name="files">
		///     A collection of files put into the zip archive.
		///     Non existing files in this collection are ignored.
		/// </param>
		/// <returns>
		///     A <see cref="MemoryStream"/> (Position = 0) with the bytes
		///     of the created zip file.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		Thrown if the passed file collection is 'null'
		/// </exception>
		/// <exception cref="ArgumentException">
		///		Thrown if the passed collection does not contain
		/// 	any valid elements.
		/// 	'valid' means, that it is not 'null' and points to
		/// 	an existing file.
		/// </exception>
		public static MemoryStream ZipFiles(IEnumerable<FileInfo> files)
		{
			var fileList = CheckArgument(files);

			var zipTargetStream = new MemoryStream();

			//Stream must be left open, since we return it to the user
			using (var targetArchive = new ZipArchive(zipTargetStream, ZipArchiveMode.Create, true))
			{
				AddFilesToArchive(targetArchive, fileList);
			}

			zipTargetStream.Position = 0;

			return zipTargetStream;
		}

		/// <summary>
		///     Creates a new <see cref="ZipArchive"/> from the
		///     passed <paramref name="files"/> and writes the
		/// 	created <see cref="ZipArchive"/> into a stream. The
		///     returned stream is not closed.
		/// </summary>
		/// <param name="files">
		///     A collection of files put into the zip archive.
		///     Non existing files in this collection are ignored.
		/// </param>
		/// <param name="onlyUniqueFiles">
		///     Only includes unique files in the created zip
		///     file. This means, that <see cref="FileInfo"/>s
		///     with the exact same <see cref="FileInfo.FullName"/>
		///     will be included just once.
		/// </param>
		/// <returns>
		///     A <see cref="MemoryStream"/> (Position = 0) with the bytes
		///     of the created zip file.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		Thrown if the passed file collection is 'null'
		/// </exception>
		/// <exception cref="ArgumentException">
		///		Thrown if the passed collection does not contain
		/// 	any valid elements.
		/// 	'valid' means, that it is not 'null' and points to
		/// 	an existing file.
		/// </exception>
		public static MemoryStream ZipFiles(IEnumerable<FileInfo> files,
		                                    bool onlyUniqueFiles)
		{
			if (onlyUniqueFiles)
			{
				var uniqueNames = new HashSet<string>(files.Where(f => f != null)
																	.Select(f => f.FullName));

				return ZipFiles(uniqueNames.Select(s => new FileInfo(s)));
			}

			return ZipFiles(files);
		}

		/// <summary>
		/// 	Unzips all files held by the <see cref="ZipArchive"/>
		/// 	inside the passed <see cref="MemoryStream"/>
		/// </summary>
		/// <param name="zipFile">
		///		A <see cref="MemoryStream"/> that contains a
		/// 	<see cref="ZipArchive"/>
		/// </param>
		/// <returns>
		///		A <see cref="MemoryStream"/> for each file contains 
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		Thrown if the passed argument is 'null'
		/// </exception>
		/// <exception cref="ArgumentException">
		///		Thrown if the passed <see cref="MemoryStream"/> does not
		/// 	contain any data
		/// </exception>
		public static IEnumerable<MemoryStream> UnzipFiles(MemoryStream zipFile)
		{
			if(zipFile is null)
				throw new ArgumentNullException(nameof(zipFile));
			if(zipFile.Length == 0)
				throw new ArgumentException($"The passed '{nameof(zipFile)}' does not contain any data");

			using (var archive = new ZipArchive(zipFile, ZipArchiveMode.Read))
			{
				foreach (var entry in archive.Entries)
				{
					yield return ExtractSingleZipEntry(entry);
				}
			}
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// 	Extracts a single <see cref="ZipArchiveEntry"/> into
		/// 	a <see cref="MemoryStream"/>
		/// </summary>
		/// <param name="entry">
		///		The <see cref="ZipArchiveEntry"/> that should be extracted
		/// </param>
		/// <returns>
		///		A <see cref="MemoryStream"/> representing the extracted
		/// 	<see cref="ZipArchiveEntry"/>
		/// </returns>
		private static MemoryStream ExtractSingleZipEntry(ZipArchiveEntry entry)
		{
			var target = new MemoryStream();

			using (var entryStream = entry.Open())
			{
				entryStream.CopyTo(target);
			}

			target.Position = 0;

			return target;
		}

		
		/// <summary>
		/// 	Checks if the passed argument is valid and
		/// 	returns a valid collection of files that can
		/// 	be zipped
		/// </summary>
		/// <param name="files">
		///		The argument that should be checked
		/// </param>
		/// <returns>
		///		A valid collection of files, that can be zipped
		/// </returns>
		/// <exception cref="ArgumentNullException">
		///		Thrown if the passed file collection is 'null'
		/// </exception>
		/// <exception cref="ArgumentException">
		///		Thrown if the passed collection does not contain
		/// 	any valid elements.
		/// 	'valid' means, that it is not 'null' and points to
		/// 	an existing file.
		/// </exception>
		private static IEnumerable<FileInfo> CheckArgument(IEnumerable<FileInfo> files)
		{
			if(files is null)
				throw new ArgumentNullException(nameof(files));
			
			var fileList = files.Where(f => f != null && f.Exists).ToList();

			if (fileList.Count == 0)
			{
				throw new ArgumentException("The enumeration does not contains any valid element",
				                            nameof(files));
			}

			return fileList;
		}
		
		/// <summary>
		///     Adds the <paramref name="files"/> to the <paramref name="targetArchive"/>
		/// </summary>
		/// <param name="targetArchive">
		///     The <see cref="ZipArchive"/> to add the files to
		/// </param>
		/// <param name="files">
		///     A collection of <see cref="FileInfo"/> added to the archive
		/// </param>
		private static void AddFilesToArchive(ZipArchive targetArchive, IEnumerable<FileInfo> files)
		{
			foreach (var file in files)
			{
				using (var entryStream = targetArchive.CreateEntry(file.Name, CompressionLevel.Optimal).Open())
				using (var writer = new BinaryWriter(entryStream))
				{
					writer.Write(File.ReadAllBytes(file.FullName));
				}
			}
		}
		
		#endregion
	}
}