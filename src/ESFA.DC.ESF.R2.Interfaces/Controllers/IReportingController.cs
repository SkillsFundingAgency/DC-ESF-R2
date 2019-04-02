﻿using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IReportingController
    {
        Task FileLevelErrorReport(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken);

        Task ProduceReports(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken);
    }
}