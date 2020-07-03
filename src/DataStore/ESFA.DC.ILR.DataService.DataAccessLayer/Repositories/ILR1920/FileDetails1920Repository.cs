using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1920EF.Rulebase;
using ESFA.DC.ILR.DataService.ILR1920EF.Valid;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1920
{
    public class FileDetails1920Repository : IFileDetails1920Repository
    {
        private readonly Func<ILR1920RulebaseContext> _1920Context;
        private readonly Func<ILR1920ValidLearnerContext> _valid1920Context;

        public FileDetails1920Repository(Func<ILR1920RulebaseContext> irl1920Context, Func<ILR1920ValidLearnerContext> valid1920Context)
        {
            _1920Context = irl1920Context;
            _valid1920Context = valid1920Context;
        }

        public async Task<ILRFileDetails> GetLatest1920FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            CollectionDetail collectionDetail;
            FileDetail fileDetail;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _1920Context())
            {
                fileDetail = await context.FileDetails
                    .Where(fd => fd.Ukprn == ukPrn)
                    .OrderByDescending(fd => fd.SubmittedTime)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            using (var validContext = _valid1920Context())
            {
                collectionDetail = await validContext.CollectionDetails
                    .Where(cd => cd.Ukprn == ukPrn)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return new ILRFileDetails()
            {
                Year = 2019,
                FileName = fileDetail?.Filename,
                LastSubmission = fileDetail?.SubmittedTime,
                FilePreparationDate = collectionDetail?.FilePreparationDate
            };
        }
    }
}