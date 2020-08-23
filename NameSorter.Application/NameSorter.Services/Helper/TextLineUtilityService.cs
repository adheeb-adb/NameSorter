using NameSorter.Domain.Contracts.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NameSorter.Services.Helper
{
    public class TextLineUtilityService : ITextLineUtilityService
    {
        public string GetLinesAsSingleString(List<string> lines)
        {
            StringBuilder lineStringBuilder = new StringBuilder();

            lines.ForEach(line => lineStringBuilder.AppendLine(line));

            // Remove the last appended new line from the lineStringBuilder
            string linesString = RemoveEmptyLines(lineStringBuilder.ToString());

            return linesString;
        }

        #region : private methods

        /// <summary>
        /// Method to remove empty lines from a string
        /// </summary>
        /// <param name="lines"> lines formatted as a single string </param>
        /// <returns> A trimmed string void of empty lines and end whitspace </returns>
        private string RemoveEmptyLines(string lines)
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", string.Empty, RegexOptions.Multiline).TrimEnd();
        }

        #endregion
    }
}
