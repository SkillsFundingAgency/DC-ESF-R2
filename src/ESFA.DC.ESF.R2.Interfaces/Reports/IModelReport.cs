﻿using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports
{
    public interface IModelReport
    {
        Task GenerateReport(
            JobContextModel jobContextModel,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken);

        string TaskName { get; }
    }
}