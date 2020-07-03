using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Services
{
    public class FileDetailsDataService : IFileDetailsDataService
    {
        private readonly IFileDetails1516Repository _repository1516;
        private readonly IFileDetails1617Repository _repository1617;
        private readonly IFileDetails1718Repository _repository1718;
        private readonly IFileDetails1819Repository _repository1819;
        private readonly IFileDetails1920Repository _repository1920;

        public FileDetailsDataService(
            IFileDetails1516Repository repository1516,
            IFileDetails1617Repository repository1617,
            IFileDetails1718Repository repository1718,
            IFileDetails1819Repository repository1819,
            IFileDetails1920Repository repository1920)
        {
            _repository1516 = repository1516;
            _repository1617 = repository1617;
            _repository1718 = repository1718;
            _repository1819 = repository1819;
            _repository1920 = repository1920;
        }

        public async Task<IList<ILRFileDetails>> GetFileDetailsForUkPrnAllYears(
            int ukPrn,
            CancellationToken cancellationToken,
            bool round2 = false)
        {
            var result = new List<ILRFileDetails>();

            ILRFileDetails fileDetails1516 = null;
            ILRFileDetails fileDetails1617 = null;
            ILRFileDetails fileDetails1718 = null;

            ILRFileDetails fileDetails1920 = null;

            if (!round2)
            {
                fileDetails1516 =
                    await _repository1516.GetLatest1516FileDetailsPerUkPrn(ukPrn, cancellationToken);

                fileDetails1617 =
                    await _repository1617.GetLatest1617FileDetailsPerUkPrn(ukPrn, cancellationToken);

                fileDetails1718 =
                    await _repository1718.GetLatest1718FileDetailsPerUkPrn(ukPrn, cancellationToken);
            }
            else
            {
                fileDetails1920 = await _repository1920.GetLatest1920FileDetailsPerUkPrn(ukPrn, cancellationToken);
            }

            var fileDetails1819 =
                await _repository1819.GetLatest1819FileDetailsPerUkPrn(ukPrn, cancellationToken);

            if (fileDetails1516 != null) { result.Add(fileDetails1516); }
            if (fileDetails1617 != null) { result.Add(fileDetails1617); }
            if (fileDetails1718 != null) { result.Add(fileDetails1718); }
            if (fileDetails1819 != null) { result.Add(fileDetails1819); }
            if (fileDetails1920 != null) { result.Add(fileDetails1920); }

            return result;
        }
    }
}