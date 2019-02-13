using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IESFProviderService
    {
        Task<IList<SupplementaryDataLooseModel>> GetESFRecordsFromFile(SourceFileModel sourceFile, CancellationToken cancellationToken);
    }
}