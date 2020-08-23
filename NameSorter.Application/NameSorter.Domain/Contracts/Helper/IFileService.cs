using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Domain.Contracts.Helper
{
    /// <summary>
    ///     Service to read from and write to files
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        ///     Method to asynchronously read all text content from a text file
        /// </summary>
        /// <param name="filePath"> A string to the path of the file </param>
        /// <returns> Returns the entire text contents of a file as a string</returns>
        Task<string> ReadTextFileAsync(string filePath);

        /// <summary>
        ///     Method to asynchronously read all lines in a text file
        /// </summary>
        /// <param name="filePath">A string to the path of the file </param>
        /// <returns> Returns all lines in a text file as a string list </returns>
        Task<List<string>> ReadAllLinesAsync(string filePath);

        /// <summary>
        ///     Method to write all contents as a single string to a text file
        /// </summary>
        /// <param name="content"> The content to be written as a string </param>
        /// <param name="filePath"> The path of the file to whcih contents need to written </param>
        /// <returns></returns>
        Task WriteToTextFileAsync(string content, string filePath);

        /// <summary>
        ///     Method to write a list of strings as lines to a text file
        /// </summary>
        /// <param name="contents"> The contents to be writtens ans list of strings </param>
        /// <param name="filePath"> The path of the file to whcih contents need to written< </param>
        /// <returns></returns>
        Task WriteLinesToTextFileAsync(List<string> contents, string filePath);
    }
}
