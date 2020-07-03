using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1516EF;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1516
{
    public class FileDetails1516Repository : IFileDetails1516Repository
    {
        private readonly Func<ILR1516Context> _1516Context;

        public FileDetails1516Repository(Func<ILR1516Context> ilr1516Context)
        {
            _1516Context = ilr1516Context;
        }

        public async Task<ILRFileDetails> GetLatest1516FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            CollectionDetail1 collectionDetail;
            FileDetail fileDetail;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _1516Context())
            {
                fileDetail = await context.FileDetails
                    .Where(fd => fd.Ukprn == ukPrn)
                    .OrderByDescending(fd => fd.SubmittedTime)
                    .FirstOrDefaultAsync(cancellationToken);

                collectionDetail = await context.CollectionDetails1
                    .Where(cd => cd.Ukprn == ukPrn)
                    .FirstOrDefaultAsync(cancellationToken);
            }

            return new ILRFileDetails()
            {
                Year = 2015,
                FileName = fileDetail?.Filename,
                LastSubmission = fileDetail?.SubmittedTime,
                FilePreparationDate = collectionDetail?.FilePreparationDate
            };
        }
    }
}