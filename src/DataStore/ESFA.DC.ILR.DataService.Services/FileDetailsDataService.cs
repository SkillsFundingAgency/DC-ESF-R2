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
        private readonly IFileDetails1819Repository _repository1819;
        private readonly IFileDetails1920Repository _repository1920;

        public FileDetailsDataService(
            IFileDetails1819Repository repository1819,
            IFileDetails1920Repository repository1920)
        {
            _repository1819 = repository1819;
            _repository1920 = repository1920;
        }

        public async Task<IList<ILRFileDetails>> GetFileDetailsForUkPrnAllYears(
            int ukPrn,
            CancellationToken cancellationToken,
            bool round2 = false)
        {
            var result = new List<ILRFileDetails>();

            var fileDetails1819 = await _repository1819.GetLatest1819FileDetailsPerUkPrn(ukPrn, cancellationToken);
            var fileDetails1920 = await _repository1920.GetLatest1920FileDetailsPerUkPrn(ukPrn, cancellationToken);

            if (fileDetails1819 != null) { result.Add(fileDetails1819); }
            if (fileDetails1920 != null) { result.Add(fileDetails1920); }

            return result;
        }
    }
}