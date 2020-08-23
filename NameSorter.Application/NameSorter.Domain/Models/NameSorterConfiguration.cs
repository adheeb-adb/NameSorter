using System;
using System.Collections.Generic;
using System.Text;

namespace NameSorter.Domain.Models
{
    public class NameSorterConfiguration
    {
        public string UnsortedNamesFilePath { get; set; }

        public string SortedNamesFilePath { get; set; }

        public int MaxAllowedNamesPerName { get; set; }
    }
}
