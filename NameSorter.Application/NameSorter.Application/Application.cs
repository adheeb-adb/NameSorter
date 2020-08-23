using NameSorter.Domain.Contracts.Application;
using NameSorter.Domain.Contracts.Helper;
using NameSorter.Domain.Contracts.Sort;
using NameSorter.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Application
{
    /// <summary>
    /// Class that contains the Name Sorter Application
    /// </summary>
    public class Application : IApplication
    {
        // constants to refer injected services and objects
        private readonly IFileService _fileService;
        private readonly ITextLineUtilityService _textLineUtilityService;
        private readonly INameSorterService _nameSorterService;
        private readonly NameSorterConfiguration _nameSorterConfiguration;
        private readonly ConsoleText _consoleText;

        public Application(
            IFileService fileService,
            ITextLineUtilityService textLineUtilityService,
            INameSorterService nameSorterService,
            NameSorterConfiguration nameSorterConfiguration,
            ConsoleText consoleText)
        {
            _fileService = fileService;
            _textLineUtilityService = textLineUtilityService;
            _nameSorterService = nameSorterService;
            _nameSorterConfiguration = nameSorterConfiguration;
            _consoleText = consoleText;
        }

        public async Task Run()
        {
            // Read the unsorted names from the specified path configured in the appSettings.json
            var unsortedNames = await GetUnsortedNames(_nameSorterConfiguration.UnsortedNamesFilePath);

            // Proceed with the sorting logic, only if there are unsorted names
            if (unsortedNames.Any())
            {
                // Sort names by its last name
                var sortedNames = _nameSorterService.SortByLastName(unsortedNames);

                // Write/Overwrite the sorted names to a text file in the path specifed in the appSettings.json
                var fileWriterTask = _fileService.WriteLinesToTextFileAsync(sortedNames, _nameSorterConfiguration.SortedNamesFilePath);

                // Write names to the console
                Console.WriteLine(_textLineUtilityService.GetLinesAsSingleString(sortedNames));

                // await for the file writer task to complete
                await fileWriterTask;

                /*
                 * Inform user that the program will exit
                 * Uncomment the line below to display the message on the console
                 */
                //Console.WriteLine(_consoleText.ExitProgram);

                // Wait for the user to exit the application
                Console.ReadLine();
            }
        }

        #region : private methods

        /// <summary>
        /// Method to read a list of names from a given path
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>  Returns the list of names or and empty list  </returns>
        private async Task<List<string>> GetUnsortedNames(string filePath)
        {
            List<string> unsortedNames = new List<string>();

            // try to read the name list from the specified path or get the list of names from a custom location
            try
            {
                unsortedNames = await _fileService.ReadAllLinesAsync(filePath);
            }
            catch (FileNotFoundException)
            {
                unsortedNames = await GetUnsortedNamesFromCustomLocation();
            }

            return unsortedNames;
        }

        /// <summary>
        /// Method to read a list of names from a path given by the User via the console
        /// </summary>
        /// <returns> Returns the list of names or and empty list </returns>
        private async Task<List<string>> GetUnsortedNamesFromCustomLocation()
        {
            List<string> unsortedNames = new List<string>();

            // Get input from user to determine if to read file from a custom path or exit
            Console.WriteLine(_consoleText.InputFileNotInDefaultPath);
            var choice = Console.ReadKey();
            Console.WriteLine();

            if (choice.KeyChar == 'c')
            {
                string filePath = string.Empty;

                do
                {
                    Console.WriteLine(_consoleText.EnterFilePath);
                    filePath = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        unsortedNames = await GetUnsortedNames(filePath);
                    }
                } while (string.IsNullOrWhiteSpace(filePath));
            }

            return unsortedNames;
        }

        #endregion
    }
}
