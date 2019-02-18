﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Database.EF.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.Logging.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class EsfRepository : IEsfRepository
    {
        private readonly Func<IESFR2Context> _contextFactory;
        private readonly ILogger _logger;

        public EsfRepository(
            Func<IESFR2Context> contextFactory,
            ILogger logger)
        {
            _contextFactory = contextFactory;
            _logger = logger;
        }

        public async Task<IList<string>> GetContractsForProvider(
            string ukPrn,
            CancellationToken cancellationToken)
        {
            List<string> contractRefNums = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _contextFactory())
                {
                    contractRefNums = await context.SourceFiles
                        .Join(context.SupplementaryDatas, sf => sf.SourceFileId, sd => sd.SourceFileId,
                            (sf, sd) => sf) // not all files will have data
                        .Where(sf => sf.Ukprn.CaseInsensitiveEquals(ukPrn))
                        .Select(sf => sf.ConRefNumber)
                        .ToListAsync(cancellationToken);
                }

                contractRefNums = contractRefNums.Distinct().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get Additional ESF SuppData source file data", ex);
            }

            return contractRefNums;
        }

        public async Task<SourceFile> PreviousFiles(string ukPrn, string conRefNumber, CancellationToken cancellationToken)
        {
            SourceFile sourceFile = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _contextFactory())
                {
                    sourceFile = await context.SourceFiles
                        .Join(context.SupplementaryDatas, sf => sf.SourceFileId, sd => sd.SourceFileId,
                            (sf, sd) => sf) // not all files will have data
                        .Where(s => s.Ukprn == ukPrn && s.ConRefNumber.CaseInsensitiveEquals(conRefNumber))
                        .FirstOrDefaultAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get ESF SuppData source file data", ex);
            }

            return sourceFile;
        }

        public async Task<IList<SourceFile>> AllPreviousFilesForValidation(
            string ukPrn,
            string conRefNum,
            CancellationToken cancellationToken)
        {
            List<SourceFile> sourceFiles = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _contextFactory())
                {
                    sourceFiles = await context.SourceFiles
                        .Where(sf => sf.Ukprn == ukPrn && sf.ConRefNumber.CaseInsensitiveEquals(conRefNum))
                        .ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get previous ESF SuppData source file data", ex);
            }

            return sourceFiles;
        }

        public async Task<IList<SupplementaryData>> GetSupplementaryDataPerSourceFile(
            int sourceFileId,
            CancellationToken cancellationToken)
        {
            List<SupplementaryData> data = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var context = _contextFactory())
                {
                    data = await context.SupplementaryDatas
                        .Where(s => s.SourceFileId == sourceFileId)
                        .ToListAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get ESF SuppData source file data", ex);
            }

            return data;
        }
    }
}