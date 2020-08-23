using System.Collections.Generic;

namespace NameSorter.Domain.Contracts.Sort
{
    /// <summary>
    ///     Service to sort names by different criteria
    /// </summary>
    public interface INameSorterService
    {
        /// <summary>
        ///     Sort names by the last name
        /// </summary>
        /// <param name="names"> string list of unsorted names</param>
        /// <returns> returns a string list of names sorted by the last name</returns>
        List<string> SortByLastName(List<string> names);
    }
}
