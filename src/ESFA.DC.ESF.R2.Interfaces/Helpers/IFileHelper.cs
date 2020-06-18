﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Helpers
{
    public interface IFileHelper
    {
        Task<IList<SupplementaryDataLooseModel>> GetESFRecords(
            IEsfJobContext esfJobContext,
            SourceFileModel sourceFileModel,
            CancellationToken cancellationToken);

        SourceFileModel GetSourceFileData(IEsfJobContext esfJobContext);
    }
}