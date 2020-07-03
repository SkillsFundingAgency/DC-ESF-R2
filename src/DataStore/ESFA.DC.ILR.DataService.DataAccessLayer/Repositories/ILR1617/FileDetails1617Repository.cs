using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1617EF;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1617
{
    public class FileDetails1617Repository : IFileDetails1617Repository
    {
        private readonly Func<ILR1617Context> _1617Context;

        public FileDetails1617Repository(Func<ILR1617Context> ilr1617Context)
        {
            _1617Context = ilr1617Context;
        }

        public async Task<ILRFileDetails> GetLatest1617FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            CollectionDetails collectionDetail;
            FileDetails fileDetail;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _1617Context())
            {
                fileDetail = await context.FileDetails
                    .Where(fd => fd.Ukprn == ukPrn)
                    .OrderByDescending(fd => fd.SubmittedTime)
                    .FirstOrDefaultAsync(cancellationToken);

                collectionDetail = await context.CollectionDetails
                    .Where(cd => cd.Ukprn == ukPrn)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return new ILRFileDetails()
            {
                Year = 2016,
                FileName = fileDetail?.Filename,
                LastSubmission = fileDetail?.SubmittedTime,
                FilePreparationDate = collectionDetail?.FilePreparationDate
            };
        }
    }
}