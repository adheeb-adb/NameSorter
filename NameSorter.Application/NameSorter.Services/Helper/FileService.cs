using NameSorter.Domain.Contracts.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NameSorter.Services.Helper
{
    public class FileService : IFileService
    {
        private readonly ITextLineUtilityService _textLineUtilityService;

        public FileService(ITextLineUtilityService textLineUtilityService)
        {
            _textLineUtilityService = textLineUtilityService;
        }

        public async Task<string> ReadTextFileAsync (string filePath)
        {
            string fileContent = string.Empty;

            using (var reader = File.ReadAllTextAsync(filePath))
            {
                fileContent = await reader;
            }

            return fileContent;
        }

        public async Task<List<string>> ReadAllLinesAsync(string filePath)
        {
            List<string> fileLines = new List<string>();

            using (var reader = File.ReadAllLinesAsync(filePath))
            {
                var fileContent = await reader;

                fileLines = fileContent.ToList();
            }

            return fileLines;
        }

        public async Task WriteToTextFileAsync (string content, string filePath)
        {
            using (var writer = File.WriteAllTextAsync(filePath, content))
            {
                await writer;
            }
        }

        public async Task WriteLinesToTextFileAsync(List<string> lines, string filePath)
        {
            string textToWrite = _textLineUtilityService.GetLinesAsSingleString(lines);

            await WriteToTextFileAsync(textToWrite, filePath);
        }
    }
}
