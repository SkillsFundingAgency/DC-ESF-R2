using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1718EF;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1718
{
    public class FileDetails1718Repository : IFileDetails1718Repository
    {
        private readonly Func<ILR1718Context> _1718Context;

        public FileDetails1718Repository(Func<ILR1718Context> irl1718Context)
        {
            _1718Context = irl1718Context;
        }

        public async Task<ILRFileDetails> GetLatest1718FileDetailsPerUkPrn(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            CollectionDetails collectionDetail;
            FileDetails fileDetail;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _1718Context())
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
                Year = 2017,
                FileName = fileDetail?.Filename,
                LastSubmission = fileDetail?.SubmittedTime,
                FilePreparationDate = collectionDetail?.FilePreparationDate
            };
        }
    }
}