using NameSorter.Domain.Contracts.Sort;
using NameSorter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NameSorter.Services.Sort
{
    public class NameSorterService : INameSorterService
    {
        private readonly NameSorterConfiguration _nameSorterConfiguration;

        public NameSorterService(NameSorterConfiguration nameSorterConfiguration)
        {
            _nameSorterConfiguration = nameSorterConfiguration;
        }

        public List<string> SortByLastName(List<string> names)
        {
            // pre-process and validate names
            var processedNames = PrePorcessAndValidateNames(names);

            // create a list of sortable objects with the processed names
            List<SortableName> sortableNames = processedNames.Select(name => new SortableName
            {
                Name = name,
                SortingName = name.Split(" ").Last()
            }).ToList();

            // Sort the names and return the sorted names as a list
            var sortedNames = SortNames(sortableNames);

            return sortedNames.ToList();
        }

        #region : private methods

        /// <summary>
        /// Method to sort a list of 'Sortable' name objects by their 'SortigName' property, and then by other names.
        /// To Sort by Last name, the last name should be assigned to the 'SortingName'.
        /// To Sort by Middle name, the middle name should be assigned to the 'SortingName' and likewise.
        /// </summary>
        /// <param name="sortableNames"> List of SortableName objects</param>
        /// <returns> returns a sorted names as a string list</returns>
        private List<string> SortNames(List<SortableName> sortableNames)
        {
            var sortedNames = sortableNames.OrderBy(sortableName => sortableName.SortingName.ToLower())
                .ThenBy(sortableName => sortableName.Name.ToLower())
                .ToList()
                .Select(sortedName => sortedName.Name);

            return sortedNames.ToList();
        }

        /// <summary>
        /// Method to preprocess the name list in roder to remove empty lines, whitspace lines and trim whitespaces
        /// </summary>
        /// <param name="unprocessedNames"> A string list of unprocessed names </param>
        /// <returns> returns a string list of names, void of empty lines, leading and trailing whitespaces </returns>
        private List<string> PrePorcessAndValidateNames(List<string> unprocessedNames)
        {
            List<string> processedNames = new List<string>();

            unprocessedNames.ForEach(name =>
            {
                /* 
                 * Ignore white spaces, empty lines
                 * Ignore names with more than 'MaxAllowedNamesPerName'(total names allowed per name) as configured in the appSettings.json
                 */
                if (!string.IsNullOrWhiteSpace(name) && name.Split(" ").Length <= _nameSorterConfiguration.MaxAllowedNamesPerName)
                {
                    // Trim leading and trailing white spaces
                    processedNames.Add(name.Trim());
                }
            });

            return processedNames;
        }

        #endregion
    }
}
