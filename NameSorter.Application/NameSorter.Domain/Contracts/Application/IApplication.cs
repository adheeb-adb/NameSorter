using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NameSorter.Domain.Contracts.Application
{
    public interface IApplication
    {
        Task Run();
    }
}
