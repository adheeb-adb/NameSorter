using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Domain.Contracts.Helper
{
    /// <summary>
    /// Utility service to manipulate texts
    /// </summary>
    public interface ITextLineUtilityService
    {
        /// <summary>
        /// Method to get a single string with new line characters, with blank lines trimmed from the end from a list of lines
        /// </summary>
        /// <param name="stringList"> A list string list of lines </param>
        /// <returns> A string formatted with line breaks and end trimmed </returns>
        string GetLinesAsSingleString(List<string> lines);
    }
}
