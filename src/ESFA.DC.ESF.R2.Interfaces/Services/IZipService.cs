using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IZipService
    {
        Task CreateZipAsync(string zipFileName, IEnumerable<string> fileNames, string container, CancellationToken cancellationToken);
    }
}
