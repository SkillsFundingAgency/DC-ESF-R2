using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1819EF.Rulebase;
using ESFA.DC.ILR.DataService.ILR1819EF.Valid;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1819
{
    public class FileDetails1819Repository : IFileDetails1819Repository
    {
        private readonly Func<ILR1819RuleBaseContext> _1819Context;
        private readonly Func<ILR1819ValidLearnerContext> _valid1819Context;

        public FileDetails1819Repository(Func<ILR1819RuleBaseContext> irl1819Context, Func<ILR1819ValidLearnerContext> valid1819Context)
        {
            _1819Context = irl1819Context;
            _valid1819Context = valid1819Context;
        }

        public async Task<ILRFileDetails> GetLatest1819FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            CollectionDetail collectionDetail;
            FileDetail fileDetail;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _1819Context())
            {
                fileDetail = await context.FileDetails
                    .Where(fd => fd.Ukprn == ukPrn)
                    .OrderByDescending(fd => fd.SubmittedTime)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            using (var validContext = _valid1819Context())
            {
                collectionDetail = await validContext.CollectionDetails
                    .Where(cd => cd.Ukprn == ukPrn)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return new ILRFileDetails()
            {
                Year = 2018,
                FileName = fileDetail?.Filename,
                LastSubmission = fileDetail?.SubmittedTime,
                FilePreparationDate = collectionDetail?.FilePreparationDate
            };
        }
    }
}